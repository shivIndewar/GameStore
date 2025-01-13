namespace GameStore.API.Dtos;

public record class GameStoreDto(
    int Id, 
    string Name, 
    string Genre, 
    decimal Price, 
    DateOnly ReleaseDate
    );
