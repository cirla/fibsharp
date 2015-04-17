namespace FibSharp

open System.Text

module Command =
    let private encoding = new ASCIIEncoding ()

    let private newLine = encoding.GetString ([| 13uy; 10uy |])

    let login clientName clipVersion username password =
        sprintf "login %s %d %s %s%s" clientName clipVersion username password newLine

    let logout =
        sprintf "bye%s" newLine

    let shout message =
        sprintf "shout %s%s" message newLine

    let kibitz message =
        sprintf "kibitz %s%s" message newLine

    let tell name message =
        sprintf "tell %s %s%s" name message newLine

    let say message =
        sprintf "say %s%s" message newLine

    let whisper message =
        sprintf "whisper %s%s" message newLine

    let message user message =
        sprintf "message %s %s%s" user message newLine
