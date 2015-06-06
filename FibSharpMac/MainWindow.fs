namespace FibSharpMac

open System.Drawing

open MonoMac.ObjCRuntime
open MonoMac.Foundation
open MonoMac.AppKit

open FibSharpMac.Controls

type LoginView (frameRect:RectangleF) as self =
    inherit NSView (frameRect)

    let usernameField = new TextField ()
    let passwordField = new SecureTextField ()

    do
        let welcomeLabel = new Label "Welcome to FibSharp!"
        self.AddSubview welcomeLabel
        Layout.centerH welcomeLabel
        Layout.topP welcomeLabel 10.0f

        let usernameLabel = new Label "Username:"
        self.AddSubview usernameLabel
        Layout.leftP usernameLabel 50.0f
        usernameLabel |> Layout.underP welcomeLabel <| 50.0f

        self.AddSubview usernameField
        Layout.leftP usernameField 50.0f
        Layout.rightP usernameField 50.0f
        usernameField |> Layout.underP usernameLabel <| 5.0f

        let passwordLabel = new Label "Password:"
        self.AddSubview passwordLabel
        Layout.leftP passwordLabel 50.0f
        passwordLabel |> Layout.underP usernameField <| 5.0f

        self.AddSubview passwordField
        Layout.leftP passwordField 50.0f
        Layout.rightP passwordField 50.0f
        passwordField |> Layout.underP passwordLabel <| 5.0f

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
