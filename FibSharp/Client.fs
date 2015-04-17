namespace FibSharp

open System.IO
open System.Net.Sockets

open Command

module Client = 

    [<Literal>]
    let ClientName = "FibSharp_v0.1"

    [<Literal>]
    let CLIPVersion = 1008

    [<Literal>]
    let FIBSHostname = "fibs.com"

    [<Literal>]
    let FIBSPort = 4321us

    type Client = {
        Stream: NetworkStream;
        Reader: StreamReader;
        Writer: StreamWriter;
    }

    let connect username password =
        let tcpClient = new System.Net.Sockets.TcpClient ()
        tcpClient.Connect (FIBSHostname, int FIBSPort)
        let stream = tcpClient.GetStream ()

        let client = {
            Stream = stream;
            Reader = new StreamReader(stream);
            Writer = new StreamWriter(stream);
        }

        let loginString =
            Command.login ClientName CLIPVersion username password

        client.Writer.Write(loginString)

        client

    let close client =
        Command.logout
        |> client.Writer.Write

        client.Stream.Close ()
