# Suave Restful API
Basic implementation of a restful API in Suave.

Followed this guide: https://blog.tamizhvendan.in/blog/2015/06/11/building-rest-api-in-fsharp-using-suave/

# Run
`dotnet run --project .\src\App\App.fsproj`

Testing POST:

  `Invoke-RestMethod -Uri http://localhost:8080/people -Method Post -Body '{"name":"Frank", "age":45, "email":"frank@test.dev"}'`
