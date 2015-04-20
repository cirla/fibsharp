namespace FibSharpTest

open System

open FsCheck
open NUnit.Framework
open Swensen.Unquote

open FibSharp.Utility

module TestMessage = 
    open FibSharp.Message

    [<Test>]
    let ``Parsing Welcome message``() =
        "1 myself 1041253132 192.168.1.308"
        |> parse =? Welcome { name = "myself";
                              lastLogin = DateTime.fromUnix 1041253132L;
                              lastHost = "192.168.1.308" }

    [<Test>]
    let ``Test parsing errors``() =
        "** Warning: You are already logged in."
        |> parse =? AlreadyLoggedIn

        "** Please use another name. 'foo' is already used by someone else."
        |> parse =? UsernameTaken "foo"

        parse "** nonsense" =? Unknown "** nonsense"
