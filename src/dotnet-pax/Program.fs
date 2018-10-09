open System

type Folder<'a,'b> = Folder of list<'a * Folder<'a,'b>> | File of 'b

// Envs folder: dev.tfvars, qa.tfvars, prod.tfvars
// Service Folder:  Envs folder, main.tf, variables.tf, output.tfvars


type Commands = Init | Service

type SubCommands = New | Update | Delete

[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"
    0 // return an integer exit code
