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
  let sqLiteVersion = 
    let workDirectory = Directory.GetCurrentDirectory();
    let cs = $"URI=file:{workDirectory}/src/App/SqliteDb/test.db"
    printfn "%s" cs

    use con = new SQLiteConnection(cs)
    con.Open()

    use cmd = new SQLiteCommand(con)

    cmd.CommandText <- "DROP TABLE IF EXISTS cars"
    cmd.ExecuteNonQuery() |> ignore

    cmd.CommandText <- @"CREATE TABLE cars(id INTEGER PRIMARY KEY, name TEXT, price INT)"
    cmd.ExecuteNonQuery() |> ignore

    cmd.CommandText <- "INSERT INTO cars(name, price) VALUES('Audi',52642)"
    cmd.ExecuteNonQuery() |> ignore

    cmd.CommandText <- "INSERT INTO cars(name, price) VALUES('Mercedes',57127)"
    cmd.ExecuteNonQuery() |> ignore

    cmd.CommandText <- "INSERT INTO cars(name, price) VALUES('Skoda',9000)"
    cmd.ExecuteNonQuery() |> ignore

    cmd.CommandText <- "INSERT INTO cars(name, price) VALUES('Volvo',29000)"
    cmd.ExecuteNonQuery() |> ignore

    cmd.CommandText <- "INSERT INTO cars(name, price) VALUES('Bentley',350000)"
    cmd.ExecuteNonQuery() |> ignore

    cmd.CommandText <- "INSERT INTO cars(name, price) VALUES('Citroen',21000)"
    cmd.ExecuteNonQuery() |> ignore

    cmd.CommandText <- "INSERT INTO cars(name, price) VALUES('Hummer',41400)"
    cmd.ExecuteNonQuery() |> ignore

    cmd.CommandText <- "INSERT INTO cars(name, price) VALUES('Volkswagen',21600)"
    cmd.ExecuteNonQuery() |> ignore
    
    "Table cars created"

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