open System
open System.IO

type Folder = File of string | Folder of string * Folder list

// Envs folder: dev.tfvars, qa.tfvars, prod.tfvars
let envsFolder = Folder ("envs", [File "dev.tfvars"; File "qa.tfvars"; File "prod.tfvars"])

// Service Folder:  Envs folder, main.tf, variables.tf, output.tfvars
let serviceFolder = Folder("service", [File "main.tf"; File "variables.tf"; File "output.tfvars"; envsFolder])

let infrastructureFolder = Folder("infrastructure", [serviceFolder])


type Commands = Init | Service

type SubCommands = New | Update | Delete


// Folder -> string
let rec folderToString folder =
    match folder with
    | File fileName -> [fileName]
    | Folder (name, []) -> [name + "/"]
    | Folder (name, otherFolders) ->
        otherFolders |> List.map folderToString |> List.collect (fun s -> s |> List.map (fun x -> name + "/" + x)) 

let createFile (fileName: string) = 
    System.IO.Directory.CreateDirectory(Path.GetDirectoryName(fileName)) |> ignore
    System.IO.File.Create(fileName).Close()

[<EntryPoint>]
let main argv =

    infrastructureFolder |> folderToString |> List.iter createFile
    
    printfn "Hello World from F#!"
    0 // return an integer exit code
