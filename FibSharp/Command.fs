namespace FibSharp

open System.Text

module Command =

    let login clientName clipVersion username password =
        sprintf "login %s %s %s %s" clientName clipVersion username password

    let logout = "bye"

    let name username =
        sprintf "name %s" username

    let shout message =
        sprintf "shout %s" message

    let kibitz message =
        sprintf "kibitz %s" message

    let tell name message =
        sprintf "tell %s %s" name message

    let say message =
        sprintf "say %s" message

    let whisper message =
        sprintf "whisper %s" message

    let message user message =
        sprintf "message %s %s" user message
