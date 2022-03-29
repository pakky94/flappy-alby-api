namespace FlappyAlby.API.Repositories;

using Abstract;
using Domain;
using DTOs;

public class RankingRepository : IRankingRepository
{
    private readonly IReader _reader;
    private readonly IWriter _writer;

    public RankingRepository(IReader reader, IWriter writer)
    {
        _reader = reader;
        _writer = writer;
    }

    public async Task<IEnumerable<PlayerDto>> GetTop10()
    {
        const string query = @"SELECT TOP 10 Name, Total as TotalMilliseconds, Id
                              FROM Player
                              ORDER BY Total ASC";

        var players = await _reader.QueryAsync<Player>(query);
        return players.Select(p => new PlayerDto(p.Name, TimeSpan.FromMilliseconds(p.TotalMilliseconds), p.Id));
    }

    public async Task<PlayerDto?> GetById(int id)
    {
        const string query = @"SELECT Name, Total as TotalMilliseconds, Id
                             FROM Player
                             WHERE Id=@Id";

        var player = await _reader.GetByIdAsync<Player>(query, id);
        if (player is null) return null;
        var playerDto = new PlayerDto(
            player.Name,
            TimeSpan.FromMilliseconds(player.TotalMilliseconds),
            player.Id
        );
        return playerDto;
    }

    public async Task<bool> Create(PlayerDto player)
    {
        const string query = @"INSERT INTO Player(Name, Total)
                             OUTPUT inserted.Id
                             VALUES (@Name, @TotalMilliseconds)";

        var playerEntity = new Player(player.Name, (long) player.Total.TotalMilliseconds, player.Id);
        _ = await _writer.CreateAsync(query, playerEntity);
        return true;
    }
}