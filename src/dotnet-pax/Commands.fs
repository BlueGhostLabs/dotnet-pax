module Commands

open Argu

type Commands = Init | Service
type SubCommands = New | Update | Delete

[<CliPrefix(CliPrefix.Dash)>]
type InitArgs = class end
with
    interface IArgParserTemplate with
        member this.Usage =
            match this with
            | _ -> "Initializes standardized terraform template strucutre."
and NewArgs =
    | Name of string
with
    interface IArgParserTemplate with
        member this.Usage =
            match this with
            | Name _ -> "Name of the new service template."
and DeleteArgs =
    | Name of string
with
    interface IArgParserTemplate with
        member this.Usage =
            match this with
            | Name _ -> "Name of the service template to delete."
and ServiceArgs =
    | [<CliPrefix(CliPrefix.None)>] New of ParseResults<NewArgs>
    | [<CliPrefix(CliPrefix.None)>] Delete of ParseResults<DeleteArgs>
with
    interface IArgParserTemplate with
        member this.Usage =
            match this with
            | New _ -> "Creates a new service template."
            | Delete _ -> "Deletes an existing service template."
and PaxArgs =
    | Version
    | [<CliPrefix(CliPrefix.None)>] Init of ParseResults<InitArgs>
    | [<CliPrefix(CliPrefix.None)>] Service of ParseResults<ServiceArgs>
with
    interface IArgParserTemplate with
        member this.Usage =
            match this with
            | Version -> "Prints the Pax version."
            | Init _ -> "Initializes standardized terraform template strucutre."
            | Service _ -> "Create a new set of templates for a specific service."
