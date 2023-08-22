namespace MusicStoreApp

open System
open Avalonia.Controls
open Avalonia.Controls.Templates
open MusicStoreApp.ViewModels


type ViewLocator() =
    
  interface IDataTemplate with
    
    member x.Build(data) =
      let name = data.GetType().FullName.Replace("ViewModel", "View")
      let typ = Type.GetType(name)
      if isNull typ then
        upcast TextBlock(Text = sprintf "Not Found: %s" name)
      else
        downcast Activator.CreateInstance(typ)
            
    member x.Match(data) = data :? ViewModelBase