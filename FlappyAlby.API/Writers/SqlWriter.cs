namespace FlappyAlby.API.Writers;

using System.Data.SqlClient;
using Abstract;
using Dapper;
using Domain;
using Microsoft.Extensions.Options;
using Options;

public class SqlWriter : IWriter
{
    private readonly string _connectionString;

    public SqlWriter(IOptions<ConnectionStringOptions> options)
    {
        _connectionString = options.Value.DefaultDatabase;
    }

    public async Task<int> CreateAsync<TEntity>(string query, TEntity entity)
        where TEntity : EntityBase
    {
        await using var connection = new SqlConnection(_connectionString);
        return await connection.ExecuteScalarAsync<int>(query, entity);
    }

    public async Task<int> UpdateAsync<TEntity>(string query, TEntity entity)
        where TEntity : EntityBase
    {
        await using var connection = new SqlConnection(_connectionString);
        return await connection.ExecuteAsync(query, entity);
    }

    public async Task<int> DeleteByIdAsync(string query, int id)
    {
        await using var connection = new SqlConnection(_connectionString);
        return await connection.ExecuteAsync(query, new {Id = id});
    }
}