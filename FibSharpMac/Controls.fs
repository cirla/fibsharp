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

    module Layout =

        let center item (view:NSView) =
            NSLayoutConstraint.Create
                (item, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal,
                 view, NSLayoutAttribute.CenterX, 1.0f, 0.0f)
            |> view.AddConstraint

            NSLayoutConstraint.Create
                (item, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal,
                 view, NSLayoutAttribute.CenterY, 1.0f, 0.0f)
            |> view.AddConstraint
