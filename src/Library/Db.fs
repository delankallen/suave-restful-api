namespace SuaveRestApi

open System.IO
open System.Collections.Generic
open System.Data.SQLite;

type Person = {
  Id : int
  Name : string
  Age : int
  Email : string
}

type Payload = {
  TypeObject : int
  TypeValue : string
}

type BatchPayload = {
  TotalObjects : int
  BatchId : string
  Payload : Payload[]
}

// {
//     "TotalObjects":3,
//     "BatchId: "158b5a56-fc24-4ecf-bd86-5ea045334cc6"
//         "Payload":[
//             {
//                  "TypeObject":2,"TypeValue":"1015"
//             },{
//                   "TypeObject":1,"TypeValue":"1024"
//             },{
//                   "TypeObject":2,"TypeValue":"2198"
//             }
//        ]
// }

module SqliteDb =
  let private openDb () = 
    let cs = $"Data Source=Automation.db;Cache=Shared"

    let con = new SQLiteConnection(cs)
    con.Open()

    let cmd = new SQLiteCommand(con)
    cmd

  let private insertBatch (cmd:SQLiteCommand) batchId =
     cmd.CommandText <- @"INSERT INTO payload_batch(batch_id, creation_date) VALUES(@batchId, DATETIME('now'))"
     cmd.Parameters.AddWithValue("@batchId", batchId) |> ignore
     cmd.Prepare()
     cmd.ExecuteNonQuery() |> ignore

  let private insertPayload (cmd:SQLiteCommand) batchId (payloadIn:Payload) =
    cmd.CommandText <- "INSERT INTO payloads(batch_id, type_object, type_value) VALUES(@batchId, @typeObject, @typeValue)"
    cmd.Parameters.AddWithValue("@batchId", batchId) |> ignore
    cmd.Parameters.AddWithValue("@typeObject", payloadIn.TypeObject) |> ignore
    cmd.Parameters.AddWithValue("@typeValue", payloadIn.TypeValue) |> ignore
    cmd.Prepare()
    cmd.ExecuteNonQuery() |> ignore

  let getPayloads batchId =
    let (newbat:Payload) = {
      TypeObject = 2
      TypeValue = "dd"
    }
    let (newPayload:BatchPayload)  = {
      TotalObjects = 2
      BatchId = "4"
      Payload = [|newbat|]
    }
    use cmd = openDb ()

    cmd.CommandText <- $"SELECT * FROM payloads WHERE batch_id = '{batchId}'"
    let returnString = cmd.ExecuteScalar()

    seq { newPayload }

  let receivePayload payloadIn =
    let (newPayload:BatchPayload)  = {
      TotalObjects = payloadIn.TotalObjects
      BatchId = payloadIn.BatchId
      Payload = payloadIn.Payload
    }

    use cmd = openDb ()

    newPayload.BatchId |> insertBatch cmd
    newPayload.Payload |> Array.iter (fun payLoad -> insertPayload cmd newPayload.BatchId payLoad)
    
    newPayload

module Db =
  let private peopleStorage = new Dictionary<int, Person>()
  let getPeople () =
    peopleStorage.Values |> Seq.map (fun p -> p)

  let createPerson person =
    let id = peopleStorage.Values.Count + 1
    let (newPerson:Person) = {
      Id = id
      Name = person.Name
      Age = person.Age
      Email = person.Email
    }

    peopleStorage.Add(id, newPerson)
    newPerson