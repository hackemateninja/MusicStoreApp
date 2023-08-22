namespace MusicStoreApp.Views

open Avalonia.Controls
open Avalonia.Interactivity
open Avalonia.Markup.Xaml
open MusicStoreApp
open MusicStoreApp.ViewModels

type MainWindow () as x = 
  inherit Window ()
  
  let vm = MainWindowViewModel()

  do x.InitializeComponent()

  member private x.InitializeComponent() =
    AvaloniaXamlLoader.Load(x)
    x.DataContext <- vm
    
  member x.OpenWindow(sender: obj)(args: RoutedEventArgs) =
    let window = MusicStoreWindow()
    window.ShowDialog(x)|>ignore