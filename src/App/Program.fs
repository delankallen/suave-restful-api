open SuaveRestApi
open Suave.Web
open Suave.Successful

[<EntryPoint>]
let main argv =
    let personWebPart = rest "people" {
      GetAll = Db.getPeople
      Create = Db.createPerson
    }

    startWebServer defaultConfig personWebPart

    0 |> ignore

    // let app =
    //     choose
    //         [GET >=> choose
    //             [ path "/math/add" >=> browse ]
    //         ]

    // let app = 
    //     choose [GET >=> choose [ path "/hello" >=> OK "Hello GET"; path "/goodbye" >=> OK "Good bye GET" ];
    //             POST >=> choose [ path "/hello" >=> OK "Hello POST"; path "/goodbye" >=> OK "Good bye POST" ] ]
 
    // startWebServer defaultConfig (mapJson (fun (calc:Calc) -> { result = calc.a + calc.b }))
    
    0 // return an integer exit code