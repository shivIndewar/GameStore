using System;
using GameStore.API.Dtos;

namespace GameStore.API.GameEndpoints;

public static class GameEndpoints
{
    private static readonly List<GameStoreDto> games =[
    new(1,"street fighter II","Fighting",19.90M,new DateOnly(1992,7,15)),
    new(2,"Final fantacy XIV","Role Playing",59.90M,new DateOnly(2010,9,30)),
    new(3,"FIFA 23","Sports",69.99M,new DateOnly(2022,9,27)),
];

public static RouteGroupBuilder MapGameEndpoints(this WebApplication app)
{
    
        var group = app.MapGroup("games");

        group.MapGet("/", ()=>games);

        // Get games/1
        group.MapGet("/{id}", (int id) => 
        {
            GameStoreDto? game = games.Find(game => game.Id == id);
            
            return game is null ? Results.NotFound() : Results.Ok(game);
            
            }).WithName("GetGame");

        //POST /games

        group.MapPost("/", (CreateGameDto newGame) =>
        {
            GameStoreDto game = new(
                games.Count + 1,
                newGame.Name,
                newGame.Genre,
                newGame.Price,
                newGame.ReleaseDate
            );
            games.Add(game);
            return Results.CreatedAtRoute("GetGame", new {id =game.Id},game);

        }).WithParameterValidation();

        //PUT games/{id}

        group.MapPut("/{id}", (int id, UpdateGameDto updateGameDto)=>
        {
            var index = games.FindIndex(game => game.Id == id);

            if(index == -1)
            {
                return Results.NotFound();
            }

            games[index] = new GameStoreDto(
                id,
                updateGameDto.Name,
                updateGameDto.Genre,
                updateGameDto.Price,
                updateGameDto.ReleaseDate
            );
            return Results.NoContent();
        });

        //Delete games/1

        group.MapDelete("/{id}",(int id)=>{
            games.RemoveAll(game=> game.Id == id);
            return Results.NoContent();
        });

    return group;
}

}
