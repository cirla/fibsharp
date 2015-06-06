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

    type TextField(?text) as self =
        inherit NSTextField()

        do
            self.TranslatesAutoresizingMaskIntoConstraints <- false
            self.StringValue <- defaultArg text ""
    
    type SecureTextField(?text) as self =
        inherit NSSecureTextField()

        do
            self.TranslatesAutoresizingMaskIntoConstraints <- false
            self.StringValue <- defaultArg text ""

    module Layout =

        let topP (item:NSView) padding =
            let view = item.Superview
            NSLayoutConstraint.Create
                (item, NSLayoutAttribute.Top, NSLayoutRelation.Equal,
                 view, NSLayoutAttribute.Top, 1.0f, padding)
            |> view.AddConstraint

        let top item =
            topP item 0.0f

        let bottomP (item:NSView) padding =
            let view = item.Superview
            NSLayoutConstraint.Create
                (item, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal,
                 view, NSLayoutAttribute.Bottom, 1.0f, -padding)
            |> view.AddConstraint

        let bottom item =
            bottomP item 0.0f

        let leftP (item:NSView) padding =
            let view = item.Superview
            NSLayoutConstraint.Create
                (item, NSLayoutAttribute.Left, NSLayoutRelation.Equal,
                 view, NSLayoutAttribute.Left, 1.0f, padding)
            |> view.AddConstraint

        let left item =
            leftP item 0.0f

        let rightP (item:NSView) padding =
            let view = item.Superview
            NSLayoutConstraint.Create
                (item, NSLayoutAttribute.Right, NSLayoutRelation.Equal,
                 view, NSLayoutAttribute.Right, 1.0f, -padding)
            |> view.AddConstraint

        let right item =
            rightP item 0.0f

        let centerH (item:NSView) =
            let view = item.Superview
            NSLayoutConstraint.Create
                (item, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal,
                 view, NSLayoutAttribute.CenterX, 1.0f, 0.0f)
            |> view.AddConstraint

        let centerV (item:NSView) =
            let view = item.Superview
            NSLayoutConstraint.Create
                (item, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal,
                 view, NSLayoutAttribute.CenterY, 1.0f, 0.0f)
            |> view.AddConstraint

        let center item =
            centerH item
            centerV item

        let aboveP (bottom:NSView) (top:NSView) padding =
            assert (top.Superview.Equals(bottom.Superview))
            let view = top.Superview
            NSLayoutConstraint.Create
                (top, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal,
                 bottom, NSLayoutAttribute.Top, 1.0f, -padding)
            |> view.AddConstraint

        let above bottom top =
            aboveP bottom top 0.0f

        let underP top bottom padding =
            aboveP bottom top padding

        let under top bottom =
            underP top bottom 0.0f
