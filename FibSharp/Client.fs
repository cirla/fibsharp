namespace FibSharp

open System.IO
open System.Net.Sockets

type Client() = 

    let mutable clip = None

    member this.register username password =
        CLIP.register username password

    member this.connect username password = 
        clip <- Some <| CLIP.connect username password

    member this.close () =
        match clip with
        | Some c ->
            CLIP.close c
            clip <- None
        | None -> ()
