namespace SuaveRestApi

[<AutoOpen>]
module Restful =
    open Newtonsoft.Json
    open Newtonsoft.Json.Serialization
    open Suave
    open Suave.Filters
    open Suave.Operators
    open Suave.Successful
    
    type RestResource<'a> = {
        GetAll : unit -> 'a seq
        Create : 'a -> 'a
    }

    let fromJson<'a> json =
        JsonConvert.DeserializeObject(json, typeof<'a>) :?> 'a

    let getResourceFromReq<'a> (req : HttpRequest) =
        let getString (rawFrom: byte[]) = 
            System.Text.Encoding.UTF8.GetString(rawFrom)
        req.rawForm |> getString |> fromJson<'a>

    let JSON v =
        let jsonSerializerSettings = JsonSerializerSettings()
        jsonSerializerSettings.ContractResolver <- CamelCasePropertyNamesContractResolver()

        JsonConvert.SerializeObject(v, jsonSerializerSettings) 
        |> OK 
        >=> Writers.setMimeType "application/json; charset=utf-8"


    let rest resourceName resource =
        let resourcePath = "/" + resourceName
        let getAll = warbler (fun _ -> resource.GetAll () |> JSON)
        path resourcePath >=> choose [
            GET >=> getAll
            POST >=> request (getResourceFromReq >> resource.Create >> JSON)
        ]