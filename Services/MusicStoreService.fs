namespace MusicStoreApp.Services

open System
open System.Diagnostics
open System.IO
open System.Net.Http
open Avalonia.Media.Imaging
open Microsoft.FSharp.Control
open MusicStoreApp.Models
open iTunesSearch.Library

module MusicStoreService =
  
  let cleanStringForFilename (input: string) =
    let invalidChars = Path.GetInvalidFileNameChars()
    let cleanChars = input.ToCharArray() |> Array.filter (fun c -> not (Array.contains c invalidChars))
    new string(cleanChars)
    
  let loadCoverBitmapAsync(artist: string)(title: string)(coverUrl: string) =
    async {
      let httpClient = new HttpClient()
      let cachePath = $"./Cache/{cleanStringForFilename artist}-{cleanStringForFilename title}"
      let bmpFilePath = cachePath + ".bmp"

      //if File.Exists(bmpFilePath) then
        //return new Bitmap(bmpFilePath)
      //else
      let! data = httpClient.GetByteArrayAsync(coverUrl) |> Async.AwaitTask
      use memoryStream = new MemoryStream(data)
      //use fileStream = File.Create(bmpFilePath)
      //memoryStream.CopyTo(fileStream)
      return new Bitmap(memoryStream)
    }
    
  let searchAsync (searchTerm: string) =
    async {
      let searchManager = iTunesSearchManager()
      let! query = searchManager.GetAlbumsAsync(searchTerm) |> Async.AwaitTask

      let! albumsAsync =
        query.Albums
        |> Seq.map (fun x ->
          async {
            let albumViewModel =
              {
                Artist = x.ArtistName
                Title = x.CollectionName
                CoverUrl = Unchecked.defaultof<Bitmap>
                
              }
            let artString = x.ArtworkUrl100.Replace("100x100bb", "600x600bb")
            Debug.WriteLine artString
            let! coverStream = loadCoverBitmapAsync albumViewModel.Artist albumViewModel.Title artString
            return { albumViewModel with CoverUrl =  coverStream }
          }
        )
        |>Async.Parallel

      return Seq.toList albumsAsync
    }