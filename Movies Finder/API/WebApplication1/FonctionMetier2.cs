using Microsoft.AspNetCore.Mvc.Diagnostics;
using WebApplication2;

namespace WebApplication1;
using Npgsql;

public class FonctionMetier2
{
    public static bool addFilm(Film data)
    {
        using (var connection = Sql.GetConnection())
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
        }

        string query = @"INSERT INTO movie (title, lead_actor, genre, duration_minutes)
                        VALUES @title, @lead_actor, @genre, @duration_minutes";
        
        using (var command = new NpgsqlCommand(query))
        {
            command.Parameters.AddWithValue("@title", data.title ?? "Titre inconnu");
            command.Parameters.AddWithValue("@lead_actor", data.lead_actor);
            command.Parameters.AddWithValue("@genre", data.genre);
            command.Parameters.AddWithValue("@duration_minutes", 
                int.TryParse(data.duration_minutes, out var duration) ? duration : (object)DBNull.Value);

            // Exécute la commande
            int rowsAffected = command.ExecuteNonQuery();

            // Retourne vrai si une ligne a été insérée
            return rowsAffected > 0;
        }
    }
}