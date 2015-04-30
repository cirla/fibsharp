namespace FibSharp

open System.IO
open System.Net.Sockets

type Client() = 

    let mutable clip = None

    member this.register username password =
        CLIP.register username password

    member this.login username password =
        clip <- Some <| CLIP.login username password

    member this.close () =
        match clip with
        | Some c ->
            CLIP.close c
            clip <- None
        | None -> ()

