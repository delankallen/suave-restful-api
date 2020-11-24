open SuaveRestApi
open Suave.Web

[<EntryPoint>]
let main argv =
    let personWebPart = rest "people" {
      GetAll = Db.getPeople
      Create = Db.createPerson
    }

    startWebServer defaultConfig personWebPart

    0