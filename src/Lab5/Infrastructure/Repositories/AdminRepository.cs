using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Abstraction;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Models.Users;
using Npgsql;

namespace Itmo.ObjectOrientedProgramming.Lab5.Infrastructure.Repositories;

public class AdminRepository(IPostgresConnectionProvider connectionProvider) : IAdminRepository
{
    public async Task<AdminData?> FindAdminById(Guid userId)
    {
        const string sql = $"""select user_id, admin_password from admins where user_id = @userId;""";

        using NpgsqlConnection connection = await connectionProvider.GetConnectionAsync(default).ConfigureAwait(false);
        using var command = new NpgsqlCommand(sql, connection);

        command.Parameters.AddWithValue("userId", userId);

        using NpgsqlDataReader reader = await command.ExecuteReaderAsync().ConfigureAwait(false);

        if (!reader.Read())
            return null;

        return new AdminData(
            reader.GetGuid(reader.GetOrdinal("user_id")),
            reader.GetString(reader.GetOrdinal("admin_password")));
    }

    public async Task Create(Guid userId, string password)
    {
        const string sql = $"""insert into admins (user_id, admin_password) values (@userId, @password);""";

        using NpgsqlConnection connection = await connectionProvider.GetConnectionAsync(default).ConfigureAwait(false);
        using var command = new NpgsqlCommand(sql, connection);

        command.Parameters.AddWithValue("userId", userId);
        command.Parameters.AddWithValue("password", password);

        await command.ExecuteNonQueryAsync().ConfigureAwait(false);
    }
}