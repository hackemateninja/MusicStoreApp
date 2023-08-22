namespace MusicStoreApp.Models

open Avalonia.Media.Imaging

type Album =
  {
    Artist: string
    Title: string
    CoverUrl: Bitmap
  }