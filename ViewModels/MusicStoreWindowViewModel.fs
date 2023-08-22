namespace MusicStoreApp.ViewModels

open System
open CommunityToolkit.Mvvm.ComponentModel
open Microsoft.FSharp.Control
open MusicStoreApp.Models
open MusicStoreApp.Services


type MusicStoreWindowViewModel() as x =
  inherit ViewModelBase()
 
  
  [<ObservableProperty>]
  let mutable _selectedAlbum = Unchecked.defaultof<Album>
  
  [<ObservableProperty>]
  let mutable _searchText = ""
  
  
  [<ObservableProperty>]
  let mutable _isBusy = false
  
  [<ObservableProperty>]
  let mutable _searchResults = []
  
  member x.SearchResults
    with get() = _searchResults
    and set value =
      x.SetProperty(&_searchResults, value)|>ignore
  
  member x.SearchText
    with get() = _searchText
    and set value =
      x.GetResults(value)
      x.SetProperty(&_searchText, value)|>ignore
      
  member x.IsBusy
    with get() = _isBusy
    and set value =
      x.SetProperty(&_isBusy, value)|>ignore
      
  member x.SelectedAlbum
    with get() = _selectedAlbum
    and set value =
      x.SetProperty(&_selectedAlbum, value)|>ignore
      
  member x.GetResults(searchTerm: string) =
    async{
      x.IsBusy <- true
      x.SearchResults <- []
      if not (String.IsNullOrWhiteSpace searchTerm)  then
        let! results =   MusicStoreService.searchAsync(x.SearchText)
        x.SearchResults <- results
       
      x.IsBusy <- false
    }|>Async.StartImmediate