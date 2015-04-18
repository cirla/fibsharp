namespace FibSharp

open System.IO
open System.Net.Sockets

open Command

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
        stream: NetworkStream;
        reader: StreamReader;
        writer: StreamWriter;
    }

    let send client (raw:string) =
        client.writer.Write raw
        client.writer.Write Command.NewLine

    let connect username password =
        let tcpClient = new System.Net.Sockets.TcpClient ()
        tcpClient.Connect (FIBSHostname, FIBSPort)
        let stream = tcpClient.GetStream ()

        let client = {
            stream = stream;
            reader = new StreamReader(stream);
            writer = new StreamWriter(stream);
        }

        Command.login ClientName SupportedVersion username password
        |> send client

        client

    let close client =
        Command.logout
        |> send client

        client.stream.Close ()

    let register username password =
        let client = connect GuestUsername GuestPassword

        Command.name username
        |> send client

        password
        |> send client

        password
        |> send client

        close client
