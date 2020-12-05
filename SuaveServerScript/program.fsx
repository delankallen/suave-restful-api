#r "nuget: Newtonsoft.Json"
#r "nuget: Suave"
#load "restful.fsx"
#load "db.fsx"

open Restful
open Db
open Suave.Web
open Suave.WebPart

let personWebPart = rest "people" {
  GetAll = Db.getPeople
  Create = Db.createPerson
}

let webhookWebPart = rest "hooks" {
  GetAll = SqliteDb.getPayloads
  Create = SqliteDb.receivePayload
}

startWebServer defaultConfig (choose [ personWebPart; webhookWebPart ])