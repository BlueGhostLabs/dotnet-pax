module Structure

open System.IO

type Folder = File of string | Folder of string * Folder list

let toTuple a b = (a,b)

let envsFolder = 
    ["dev.tfvars"; "qa.tfvars"; "prod.tfvars"]
    |> List.map File
    |> toTuple "envs"
    |> Folder

let service serviceName =
    [File "main.tf"; File "variables.tf"; File "output.tfvars"; envsFolder]
    |> toTuple serviceName
    |> Folder
    
let infrastructureFolder serviceFolder = Folder("infrastructure", [serviceFolder])

// type FolderPrinter = FolderPrinter of (Folder -> string list)


let rec folderToString folder =
    match folder with
    | File fileName -> [fileName]
    | Folder (name, []) -> [name + "/"]
    | Folder (name, otherFolders) ->
        otherFolders |> List.map folderToString |> List.collect (fun s -> s |> List.map (fun x -> name + "/" + x)) 

let createFile (fileName: string) = 
    Directory.CreateDirectory(Path.GetDirectoryName(fileName)) |> ignore
    File.Create(fileName).Close()