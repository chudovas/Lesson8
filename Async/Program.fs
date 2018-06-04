open System.Net
open System.IO
open System.Text.RegularExpressions

let PrintStatisticForUrl url =
    let urlRegex = Regex("<a href\s*=\s*\"(https?://[^\"]+)\"\s*>", RegexOptions.Compiled)
    
    let DownloadPage (url : string) = 
        async {
            try
                let request = WebRequest.Create(url)
                use! response = request.AsyncGetResponse()
                use stream = response.GetResponseStream()
                use reader = new StreamReader(stream)
                let text = reader.ReadToEnd() 
                return (url, true, text)
            with
            | error -> return (url, false, error.Message)
        }
    
    let mainPageText = url |> DownloadPage |> Async.RunSynchronously
    
    match mainPageText with
    | (_, false, errText) -> printfn "Error with main url! %s" errText
    | (_, true, text) ->
        let allSitesAndTexts = [for m in urlRegex.Matches(text) -> DownloadPage(m.Groups.[1].Value)] |> Async.Parallel |> Async.RunSynchronously
        for el in allSitesAndTexts do
            match el with
            | (url, false, errText) -> printfn "Error with %s url! %s" url errText
            | (url, true, text) -> printfn "%s --- %d" url text.Length

[<EntryPoint>]
let main argv =
    0 
