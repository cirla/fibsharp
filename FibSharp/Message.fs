namespace FibSharp

open System

open FibSharp.Utility

module Message =

    type Welcome = {
        name      : string;
        lastLogin : DateTime;
        lastHost  : string;
    }

    let welcome (args:string[]) =
        match args.Length with
        | 3 -> { name = args.[0];
                 lastLogin = DateTime.fromUnix (System.Int64.Parse args.[1]);
                 lastHost = args.[2] }
        | _ -> invalidArg "args" (sprintf "Expected 3 arguments, got %d." args.Length)

    type Redoubles =
        | None
        | Unlimited
        | Value of int

    let redoubles = function
        | "none" -> None
        | "unlimited" -> Unlimited
        | Regex @"(\d)" [value] -> System.Int32.Parse value |> Value
        | _ -> invalidArg "value" "Valid values are 0-99, 'none', and 'unlimited'"

    type OwnInfo = {
        name       : string;
        allowPip   : bool;
        autoBoard  : bool;
        autoDouble : bool;
        autoMove   : bool;
        away       : bool;
        bell       : bool;
        crawford   : bool;
        double     : bool;
        experience : int;
        greedy     : bool;
        moreBoards : bool;
        moves      : bool;
        notify     : bool;
        rating     : float;
        ratings    : bool;
        ready      : bool;
        redoubles  : Redoubles;
        report     : bool;
        silent     : bool;
        timeZone   : TimeZone;
    }

    type Message =
        | Welcome of Welcome
        | OwnInfo
        | MOTDBegin
        | MOTDEnd
        | WhoInfo
        | WhoInfoEnd
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
        | AlreadyLoggedIn
        | UsernameTaken of string
        | InvalidUsername
        | NoPasswordGiven
        | PasswordMismatch
        | UnknownCommand
        | Unknown of string
        | Empty

    let private parseMessage code (message:string) =
        let args = message.Split [|' '|]

        match code with
        | 1  -> Welcome (welcome args)
        | 2  -> OwnInfo
        | 3  -> MOTDBegin
        | 4  -> MOTDEnd
        | 5  -> WhoInfo
        | 6  -> WhoInfoEnd
        | 7  -> Login
        | 8  -> Logout
        | 9  -> MessageReceived
        | 10 -> MessageDelivered
        | 11 -> MessageSaved
        | 12 -> Says
        | 13 -> Shouts
        | 14 -> Whispers
        | 15 -> Kibitzes
        | 16 -> YouSay
        | 17 -> YouShout
        | 18 -> YouWhisper
        | 19 -> YouKibitz
        | _ -> sprintf "%d %s" code message |> Unknown

    let private parseOther message =
        match message with
        | "** Warning: You are already logged in."
            -> AlreadyLoggedIn
        | Regex @"^\*\* Please use another name. '(.+?)' is already used by someone else\.$" [ username ]
            -> UsernameTaken username
        | _ -> Unknown message

    let parse message =
        match message with
        | "" -> Empty
        | Regex @"^(\d+) ?(.*)$" [code; message] -> parseMessage (Int32.Parse code) message
        | _ -> parseOther message

