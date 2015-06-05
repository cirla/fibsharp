namespace FibSharpMac

open System
open System.Drawing

open MonoMac.ObjCRuntime
open MonoMac.Foundation
open MonoMac.AppKit

open FibSharp
open FibSharpMac.Controls

type AboutWindow () as self =
    inherit NSWindow ()

    do
        self.StyleMask <- NSWindowStyle.Closable ||| NSWindowStyle.Miniaturizable ||| NSWindowStyle.Titled
        self.SetFrame (new RectangleF (0.0f, 0.0f, 400.0f, 400.0f), false)
        self.Center ()

        // override close button to hide window
        let closeButton = self.StandardWindowButton NSWindowButton.CloseButton
        closeButton.Action <- new Selector "orderOut:"

        let view = self.ContentView
        let label = new Label "FibSharp"
        view.AddSubview label

        Layout.center label view

type FibSharpAppDelegate () =
    inherit NSApplicationDelegate ()

    let aboutWindow = new AboutWindow ()
    let mainController = new MainWindowController ()

    let quitHandler (sender:obj) (e:EventArgs) =
        NSApplication.SharedApplication.Terminate (sender :?> NSObject)

    let createMainMenu appName =
        let mainMenu = new NSMenu ()

        let appMenuItem = new NSMenuItem ()
        let appMenu = new NSMenu ()

        appMenu.AddItem (new NSMenuItem (sprintf "About %s" appName, (fun sender e ->
            aboutWindow.MakeKeyAndOrderFront mainMenu)))
        appMenu.AddItem (new NSMenuItem ("Check for Updates...", (fun sender e -> () )))
        appMenu.AddItem NSMenuItem.SeparatorItem
        appMenu.AddItem (new NSMenuItem ("Preferences...", ",", (fun sender e -> () )))
        appMenu.AddItem NSMenuItem.SeparatorItem
        appMenu.AddItem (new NSMenuItem (sprintf "Connect", "c", (fun sender e -> () )))
        appMenu.AddItem NSMenuItem.SeparatorItem
        appMenu.AddItem (new NSMenuItem (sprintf "Quit", "q", quitHandler))

        appMenuItem.Submenu <- appMenu
        mainMenu.AddItem appMenuItem

        mainMenu

    override self.FinishedLaunching notification =
        NSApplication.SharedApplication.MainMenu <- createMainMenu NSProcessInfo.ProcessInfo.ProcessName
        mainController.ShowWindow self

module main =
    [<EntryPoint>]
    let main args =
        NSApplication.Init ()

        let app = NSApplication.SharedApplication
        app.Delegate <- new FibSharpAppDelegate ()
        app.Run ()
        0

