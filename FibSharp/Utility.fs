namespace FibSharp

open System
open System.Text.RegularExpressions

module Utility =
    module DateTime =
        let Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
        let fromUnix (unixTime:int64) = Epoch.AddSeconds (float unixTime)

    let (|Regex|_|) pattern input =
        let m = Regex.Match (input, pattern)
        if m.Success then Some(List.tail [ for g in m.Groups -> g.Value ]) else None

