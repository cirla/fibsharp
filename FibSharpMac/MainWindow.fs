namespace FibSharpMac

open System.Drawing

open MonoMac.ObjCRuntime
open MonoMac.Foundation
open MonoMac.AppKit

open FibSharpMac.Controls

type LoginView (frameRect:RectangleF) as self =
    inherit NSView (frameRect)

    do
        let welcomeLabel = new Label "Welcome to FibSharp!"
        self.AddSubview welcomeLabel

        Layout.center welcomeLabel self

type MainWindow =
    inherit NSWindow

    new () = { inherit NSWindow() }

    new (contentRect, style, bufferingType, deferCreation) =
        { inherit NSWindow (contentRect, style, bufferingType, deferCreation) }

type MainWindowController () as self =
    inherit NSWindowController ()

    do
        let rect = new RectangleF (0.0f, 0.0f, 640.0f, 480.0f)
        self.Window <- new MainWindow (
            rect,
            NSWindowStyle.Titled ||| NSWindowStyle.Resizable ||| NSWindowStyle.Closable ||| NSWindowStyle.Miniaturizable,
            NSBackingStore.Buffered,
            false)
        self.Window.Center ()

        let view = self.Window.ContentView

        let loginView = new LoginView (view.Frame)
        view.AddSubview loginView
