namespace FlappyAlby.API.Abstract;

using System.Data;
using Domain;

public interface IReader
{
    public Task<IEnumerable<TEntity>> QueryAsync<TEntity>(string query)
        where TEntity : EntityBase;
    public Task<TEntity?> GetByIdAsync<TEntity>(string query, int id)
        where TEntity : EntityBase;
}