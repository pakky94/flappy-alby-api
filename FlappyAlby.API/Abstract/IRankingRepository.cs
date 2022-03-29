namespace FlappyAlby.API.Abstract;

using DTOs;

public interface IRankingRepository
{
    Task<IEnumerable<PlayerDto>> GetTop10();
    Task<PlayerDto?> GetById(int id);
    Task<bool> Create(PlayerDto player);
}