# Suave Restful API
Basic implementation of a restful API in Suave.

Followed this guide: https://blog.tamizhvendan.in/blog/2015/06/11/building-rest-api-in-fsharp-using-suave/

Contains an SqlLite database. Creation script: [create_table.sql](src/Library/create_table.sql)

I designed this to be a simple server to consume and store webhook payloads.

Example payload:

```
  {
    "TotalObjects":3,
    "BatchId: "158b5a56-fc24-4ecf-bd86-5ea045334cc6",
    "Payload":[
          {
                "TypeObject":2,"TypeValue":"1015"
          },
          {
                "TypeObject":1,"TypeValue":"1024"
          },
          {
                "TypeObject":2,"TypeValue":"2198"
          }
       ]
  }
```

# Run
`dotnet run --project .\src\App\App.fsproj`

Testing POST:

  `Invoke-RestMethod -Uri http://localhost:8080/hooks -Method Post -Body '{"TotalObjects":1,"BatchId":"158b5a56-fc24-4ecf-bd86-5ea045334cc6","Payload":[{"TypeObject":2,"TypeValue":"1015"}]}'`

Testing GET:
`Invoke-RestMethod -Uri http://localhost:8080/hooks -Method Get`
