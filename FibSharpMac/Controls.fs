namespace FibSharpMac

open System.Drawing

open MonoMac.ObjCRuntime
open MonoMac.Foundation
open MonoMac.AppKit

module Controls =

    type Label(text) as self =
        inherit NSTextField()

        do
            self.TranslatesAutoresizingMaskIntoConstraints <- false
            self.StringValue <- text
            self.Bezeled <- false
            self.DrawsBackground <- false
            self.Editable <- false

