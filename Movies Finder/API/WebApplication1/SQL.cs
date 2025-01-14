using System;
using Npgsql;
using WebApplication1;

namespace WebApplication2;

public class Sql
{
    public static NpgsqlConnection GetConnection()
    {
        string host = "localhost";
        string port = "5432";
        string database = "ma_base";
        string username = "mon_user";
        string password = "mon_password";

        var connectionString = $"Host={host};Port={port};Database={database};Username={username};Password={password};";

        var connection = new NpgsqlConnection(connectionString);
        connection.Open();
        return connection;
    }
}

