using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Abstraction;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Models.Operations;
using Npgsql;

namespace Itmo.ObjectOrientedProgramming.Lab5.Infrastructure.Repositories;

public class OperationRepository(IPostgresConnectionProvider connectionProvider) : IOperationRepository
{
    public async Task<IEnumerable<OperationData>> GetUserOperationHistory(Guid userId)
    {
        const string sql = $"""select user_id, type, status, operation_date, amount from operations where user_id = @userId;""";

        using NpgsqlConnection connection = await connectionProvider.GetConnectionAsync(default).ConfigureAwait(false);
        using var command = new NpgsqlCommand(sql, connection);

        command.Parameters.AddWithValue("userId", userId);

        using NpgsqlDataReader reader = await command.ExecuteReaderAsync().ConfigureAwait(false);

        var operations = new List<OperationData>();

        while (reader.Read())
        {
            operations.Add(new OperationData(
                UserId: reader.GetGuid(reader.GetOrdinal("user_id")),
                Type: reader.GetFieldValue<OperationType>(reader.GetOrdinal("type")),
                Status: reader.GetFieldValue<OperationStatus>(reader.GetOrdinal("status")),
                Date: reader.GetDateTime(reader.GetOrdinal("operation_date")),
                Amount: reader.GetDecimal(reader.GetOrdinal("amount"))));
        }

        return operations;
    }

    public async Task<IEnumerable<OperationData>> GetOperationHistory()
    {
        const string sql = $"""select user_id, type, status, operation_date, amount from operations;""";

        using NpgsqlConnection connection = await connectionProvider.GetConnectionAsync(default).ConfigureAwait(false);
        using var command = new NpgsqlCommand(sql, connection);

        using NpgsqlDataReader reader = await command.ExecuteReaderAsync().ConfigureAwait(false);

        var operations = new List<OperationData>();

        while (reader.Read())
        {
            operations.Add(new OperationData(
                UserId: reader.GetGuid(reader.GetOrdinal("user_id")),
                Type: reader.GetFieldValue<OperationType>(reader.GetOrdinal("type")),
                Status: reader.GetFieldValue<OperationStatus>(reader.GetOrdinal("status")),
                Date: reader.GetDateTime(reader.GetOrdinal("operation_date")),
                Amount: reader.GetDecimal(reader.GetOrdinal("amount"))));
        }

        return operations;
    }

    public async Task AddOperationData(OperationData operationData)
    {
        const string sql = $"""
                            insert into operations (user_id, type, status, operation_date, amount)
                            values (@userId, @type, @status, @date, @amount);
                            """;

        using NpgsqlConnection connection = await connectionProvider.GetConnectionAsync(default).ConfigureAwait(false);
        using var command = new NpgsqlCommand(sql, connection);

        command.Parameters.AddWithValue("userId", operationData.UserId);
        command.Parameters.AddWithValue("type", operationData.Type);
        command.Parameters.AddWithValue("status", operationData.Status);
        command.Parameters.AddWithValue("date", operationData.Date);
        command.Parameters.AddWithValue("amount", operationData.Amount);

        await command.ExecuteNonQueryAsync().ConfigureAwait(false);
    }
}