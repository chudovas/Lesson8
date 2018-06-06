open System

open NUnit.Framework
open FsUnit
open Async

[<Test>]
let ``test of correct regex 1``() =
    findAllUrlInText "djnfvksjnfvjanvjfshbdvofohbvofhbvohd" |> should equal []

[<Test>]
let ``test of correct regex 2``() =
    findAllUrlInText "<a href=\"http://1\"><a href=\"http://2\"><a href=\"http://3\">" |> should equal ["http://1"; "http://2"; "http://3"]

[<Test>]
let ``test of fail on uncorrect url``() =
    printStatisticForUrl "jfnkjvndfkv" |> should equal false

[<Test>]
let ``test of loading page``() =
    let (_, _, text) = ("https://yandex.ru" |> downloadPage |> Async.RunSynchronously)
    (text.Length > 0) |> should equal true

[<EntryPoint>]
let main argv =
    ``test of correct regex 1``()
    ``test of correct regex 2``()
    ``test of fail on uncorrect url``()
    ``test of loading page``()
    0
