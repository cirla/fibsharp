namespace FibSharp

open System
open System.IO
open System.Net.Sockets
open System.Text

module CLIP =

    [<Literal>]
    let ClientName = "FibSharp_v0.1"

    [<Literal>]
    let SupportedVersion = "1008"

    [<Literal>]
    let FIBSHostname = "fibs.com"

    [<Literal>]
    let FIBSPort = 4321

    [<Literal>]
    let GuestUsername = "guest"

    [<Literal>]
    let GuestPassword = "guest"

    type Client = {
        stream: Stream;
        reader: StreamReader;
        writer: StreamWriter;
    }

    let private client stream =
        let s = new BufferedStream(stream)
        { stream = s;
          reader = new StreamReader(s);
          writer = new StreamWriter(s) }

    let send client (raw:string) =
        sprintf "%s\r\n" raw |> client.writer.Write
        client.writer.Flush ()

    let private recv client =
        try
            let raw = client.reader.ReadLine ()
            raw.TrimEnd () |> Some
        with
            | :? IOException as e -> None

    let private recvAll client =
        let rec loop acc = function
            | None -> acc
            | Some x -> loop (x::acc) (recv client)
        loop [] (recv client)
        |> List.rev

    let rec private recvMsg client =
        match recv client with
        | Some message -> match Message.parse message with
                          | Message.Empty -> recvMsg client
                          | m -> Some m
        | None -> None

    let private connect () =
        let tcpClient = new System.Net.Sockets.TcpClient ()
        tcpClient.Connect (FIBSHostname, FIBSPort)
        let stream = tcpClient.GetStream ()
        stream.ReadTimeout <- 1000
        let client = client stream

        let banner = recvAll client
        String.concat "\n" banner
        |> printfn "%s"

        client

    let login username password =
        let client = connect ()

        Command.login ClientName SupportedVersion username password
        |> send client

        match recvMsg client with
        | None -> failwith "Invalid Password"
        | Some m -> match m with
                    | Message.Welcome welcome -> printf "Welcome, %s" welcome.name
                    | Message.AlreadyLoggedIn -> printf "Already Logged In"
                    | m -> failwithf "Unexpected Message: %A" m

        client

    let close client =
        Command.logout
        |> send client

        client.stream.Close ()

    let register username password =
        let client = connect ()

        GuestUsername |> send client
        GuestPassword |> send client
        Command.name username |> send client
        password |> send client
        password |> send client

        close client

