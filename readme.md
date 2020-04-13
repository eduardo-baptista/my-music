# :musical_score: My Music :musical_score:

A simple ASP.NET Core app build to pratic most used patterns and multi layer.

## Techs

This project was made with the following technologies:

- [ASP.NET Core 3.1](https://dotnet.microsoft.com/)
- [EF Core](https://docs.microsoft.com/pt-br/ef/#pivot=efcore)
- [AutoMapper](https://automapper.org/)
- [FluentValidation](https://fluentvalidation.net/)
- [Swagger](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)

## Launch

To launch the app just clone and exec the follow comands inside the project folder:

```
docker-compose up -d
dotnet ef -s MyMusic.Api/MyMusic.Api.csproj database update
```

## Docs

After launch the app go to http://localhost:5000/docs
