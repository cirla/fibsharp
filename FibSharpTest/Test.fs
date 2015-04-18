namespace FibSharpTest

open FsCheck
open NUnit.Framework
open Swensen.Unquote

open FibSharp.Utility

module TestMessage = 
    open FibSharp.Message

    [<Test>]
    let ``Parsing Welcome message``() =
        "1 myself 1041253132 192.168.1.308"
        |> parse =? Right (Info Welcome)

    [<Test>]
    let ``Test parsing errors``() =
        "** Warning: You are already logged in."
        |> parse =? Right (Error AlreadyLoggedIn)

        "** Please use another name. 'foo' is already used by someone else."
        |> parse =? Right (Error (UsernameTaken "foo"))

        parse "** nonsense" =? Left (UnknownError "nonsense")
