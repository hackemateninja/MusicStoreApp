namespace MusicStoreApp

open Avalonia.Controls
open Avalonia.Markup.Xaml
open MusicStoreApp.ViewModels

type MusicStoreWindow () as x = 
  inherit Window ()

  let vm = MusicStoreWindowViewModel()
  
  do
    x.InitializeComponent()

  member private x.InitializeComponent() =
    AvaloniaXamlLoader.Load(x)
    x.DataContext <- vm
