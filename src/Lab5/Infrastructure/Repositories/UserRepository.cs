using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Abstraction;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Models.Users;
using Npgsql;

namespace Itmo.ObjectOrientedProgramming.Lab5.Infrastructure.Repositories;

public class UserRepository(IPostgresConnectionProvider connectionProvider) : IUserRepository
{
    public async Task<UserData?> FindUserById(Guid userId)
    {
        const string sql = $"""select user_id, user_pin, user_balance from users where user_id = @userId;""";

        using NpgsqlConnection connection = await connectionProvider.GetConnectionAsync(default).ConfigureAwait(false);
        using var command = new NpgsqlCommand(sql, connection);

        command.Parameters.AddWithValue("userId", userId);

        using NpgsqlDataReader reader = await command.ExecuteReaderAsync().ConfigureAwait(false);

        if (!reader.Read())
            return null;

        return new UserData(
            reader.GetGuid(reader.GetOrdinal("user_id")),
            reader.GetString(reader.GetOrdinal("user_pin")),
            reader.GetDecimal(reader.GetOrdinal("user_balance")));
    }

    public async Task Create(Guid userId, string pin)
    {
        const decimal balance = decimal.Zero;

        const string sql = $"""insert into users (user_id, user_pin, user_balance) values (@userId, @pin, @balance);""";

        using NpgsqlConnection connection = await connectionProvider.GetConnectionAsync(default).ConfigureAwait(false);
        using var command = new NpgsqlCommand(sql, connection);

        command.Parameters.AddWithValue("userId", userId);
        command.Parameters.AddWithValue("pin", pin);
        command.Parameters.AddWithValue("balance", balance);

        await command.ExecuteNonQueryAsync().ConfigureAwait(false);
    }

    public async Task<decimal> GetBalance(Guid userId)
    {
        const string sql = $"""select user_balance from users where user_id = @userId;""";

        using NpgsqlConnection connection = await connectionProvider.GetConnectionAsync(default).ConfigureAwait(false);
        using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue("userId", userId);

        using NpgsqlDataReader reader = await command.ExecuteReaderAsync().ConfigureAwait(false);

        if (!reader.Read())
        {
            throw new Exception("User balance operation failed: user not found.");
        }

        return reader.GetDecimal(reader.GetOrdinal("user_balance"));
    }

    public async Task Withdraw(Guid userId, decimal amount)
    {
        decimal balance = await GetBalance(userId).ConfigureAwait(false) - amount;

        const string sql = $"""update users set user_balance = @balance where user_id = @userId;""";

        using NpgsqlConnection connection = await connectionProvider.GetConnectionAsync(default).ConfigureAwait(false);
        using var command = new NpgsqlCommand(sql, connection);

        command.Parameters.AddWithValue("userId", userId);
        command.Parameters.AddWithValue("balance", balance);

        await command.ExecuteNonQueryAsync().ConfigureAwait(false);
    }

    public async Task Deposit(Guid userId, decimal amount)
    {
        decimal balance = await GetBalance(userId).ConfigureAwait(false) + amount;

        const string sql = $"""update users set user_balance = @balance where user_id = @userId;""";

        using NpgsqlConnection connection = await connectionProvider.GetConnectionAsync(default).ConfigureAwait(false);
        using var command = new NpgsqlCommand(sql, connection);

        command.Parameters.AddWithValue("userId", userId);
        command.Parameters.AddWithValue("balance", balance);

        await command.ExecuteNonQueryAsync().ConfigureAwait(false);
    }
}