namespace FibSharpTest

open FsCheck
open NUnit.Framework
open Swensen.Unquote

open FibSharp.Utility

module TestMessage = 
    open FibSharp.Message

    [<Test>]
    let ``Parsing Welcome message``() =

        parse "1 myself 1041253132 192.168.1.308"
            =? Right (Info Welcome)

    [<Test>]
    let ``Test parsing errors``() =

        parse "** Warning: You are already logged in."
            =? Right (Error AlreadyLoggedIn)
    
        parse "** Please use another name. 'foo' is already used by someone else."
            =? Right (Error (UsernameTaken "foo"))

        parse "** nonsense" =? Left (UnknownError "nonsense")
