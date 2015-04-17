namespace FibSharp

open System.Text.RegularExpressions

module Utility =
    type Either<'a, 'b> = Choice<'b, 'a>
    let Right x :Either<'a, 'b> = Choice1Of2 x
    let Left x :Either<'a, 'b> = Choice2Of2 x
    let (|Right|Left|) = function
        | Choice1Of2 x -> Right x
        | Choice2Of2 x -> Left x

    let (|Regex|_|) pattern input =
        let m = Regex.Match (input, pattern)
        if m.Success then Some(List.tail [ for g in m.Groups -> g.Value ]) else None
