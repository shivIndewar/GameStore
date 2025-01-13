using GameStore.API.Dtos;
using GameStore.API.GameEndpoints;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
    app.MapGameEndpoints();

app.Run();
