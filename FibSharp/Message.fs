namespace FibSharp

open System

open FibSharp.Utility

module Message =

    type Info =
        | Welcome
        | OwnInfo
        | MOTD
        | WhoInfo
        | Login
        | Logout
        | MessageReceived
        | MessageDelivered
        | MessageSaved
        | Says
        | Shouts
        | Whispers
        | Kibitzes
        | YouSay
        | YouShout
        | YouWhisper
        | YouKibitz

    type Error =
        | AlreadyLoggedIn
        | UsernameTaken of string
        | InvalidUsername
        | NoPasswordGiven
        | PasswordMismatch
        | UnknownCommand of string

    type Message =
        | Info of Info
        | Error of Error

    type ParseError =
        | UnknownError of string
        | UnknownMessage of string
        | InvalidArguments

    type ParseResult = Either<ParseError, Message>

    let private parseMessage code message =
        match code with
        | 1 -> Right (Info Welcome)
        | 2 -> Right (Info OwnInfo)
        | _ -> Left (UnknownMessage message)

    let private parseError message =
        match message with
        | "Warning: You are already logged in."
            -> Right (Error AlreadyLoggedIn)
        | Regex @"Please use another name. '(.+?)' is already used by someone else\." [ username ]
            -> Right (Error (UsernameTaken username))
        | error -> Left (UnknownError error)

    let private (|Error|_|) (message:string) =
        let prefix = "** "
        match message.StartsWith prefix with 
        | true -> Some (message.Substring prefix.Length)
        | false -> None

    let parse message :ParseResult =
        match message with
        | Regex @"(\d) ?(.*)" [code; message] -> parseMessage (Int32.Parse code) message
        | Error error -> parseError error
        | _ -> Left (UnknownMessage message)
