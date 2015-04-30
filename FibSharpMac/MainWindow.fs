namespace FibSharpMac

open System.Drawing

open MonoMac.ObjCRuntime
open MonoMac.Foundation
open MonoMac.AppKit

open FibSharpMac.Controls

type MainWindow =
    inherit NSWindow

    new () = { inherit NSWindow() }

    new (contentRect, style, bufferingType, deferCreation) =
        { inherit NSWindow (contentRect, style, bufferingType, deferCreation) }

type MainWindowController() as self =
    inherit NSWindowController()

    do
        let rect = new RectangleF (0.0f, 0.0f, 640.0f, 480.0f)
        self.Window <- new MainWindow (
            rect,
            NSWindowStyle.Titled ||| NSWindowStyle.Resizable ||| NSWindowStyle.Closable ||| NSWindowStyle.Miniaturizable,
            NSBackingStore.Buffered,
            false)
        self.Window.Center ()

        let view = self.Window.ContentView
        let label = new Label "Welcome to FibSharp!"
        view.AddSubview label

        NSLayoutConstraint.Create
            (label, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal,
             view, NSLayoutAttribute.CenterX, 1.0f, 0.0f)
        |> view.AddConstraint

        NSLayoutConstraint.Create
            (label, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal,
             view, NSLayoutAttribute.CenterY, 1.0f, 0.0f)
        |> view.AddConstraint

