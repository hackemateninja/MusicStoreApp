namespace MusicStoreApp

open Avalonia
open Avalonia.Controls.ApplicationLifetimes
open Avalonia.Markup.Xaml
open MusicStoreApp.ViewModels
open MusicStoreApp.Views

type App() =
  inherit Application()

  override x.Initialize() =
    AvaloniaXamlLoader.Load(x)

  override x.OnFrameworkInitializationCompleted() =
    match x.ApplicationLifetime with
    | :? IClassicDesktopStyleApplicationLifetime as desktop ->
         desktop.MainWindow <- MainWindow(DataContext = MainWindowViewModel())
    | _ -> ()

    base.OnFrameworkInitializationCompleted()