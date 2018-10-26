open Structure
open Commands


[<EntryPoint>]
let main argv =
    match Array.tryHead argv with
    | Some serviceName -> serviceName |> service |> infrastructureFolder |> folderToString |> List.iter createFile
    | None -> printfn "No service name provided!"
    0 // return an integer exit code
