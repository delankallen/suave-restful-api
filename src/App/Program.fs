open SuaveRestApi
open Suave.Web

[<EntryPoint>]
let main argv =
    let personWebPart = rest "people" {
      GetAll = Db.getPeople
      Create = Db.createPerson
    }

    let webhookWebPart = rest "hooks" {
      GetAll = SqliteDb.getPayloads
      Create = SqliteDb.receivePayload
    }

    startWebServer defaultConfig webhookWebPart

    0