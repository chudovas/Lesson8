open System

open NUnit.Framework
open FsUnit
open Async

[<Test>]
let ``test of correct regex 1``() =
    findAllUrlInTex "djnfvksjnfvjanvjfshbdvofohbvofhbvohd" |> should equal []

[<Test>]
let ``test of correct regex 2``() =
    findAllUrlInTex "<a href=\"http://1\"><a href=\"http://2\"><a href=\"http://3\">" |> should equal ["http://1"; "http://2"; "http://3"]

[<Test>]
let ``test of fail on uncorrect url``() =
    printStatisticForUrl "jfnkjvndfkv" |> should equal false

[<EntryPoint>]
let main argv =
    ``test of correct regex 1``()
    ``test of correct regex 2``()
    ``test of fail on uncorrect url``()
    0
