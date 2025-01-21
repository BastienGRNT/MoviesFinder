using Npgsql;
using NpgsqlTypes;
using WebApplication2;

namespace WebApplication1;

public class FonctionMetier
{
    public static List<Film> RecherFilm(Film film)
    {

        var filmT = new List<Film>();

        using (var connection = Sql.GetConnection())
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }

            string query = $@"SELECT title, lead_actor, genre, duration_minutes 
                              FROM movies 
                              WHERE title ILIKE @titre 
                              AND lead_actor ILIKE @actor 
                              AND genre ILIKE @genre 
                              AND (@minutes IS NULL OR duration_minutes <= @minutes::INTEGER);";

            using (var cmd = new NpgsqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@titre", $"%{film.title}%");
                cmd.Parameters.AddWithValue("@actor", $"%{film.lead_actor}%");
                cmd.Parameters.AddWithValue("@genre", $"%{film.genre}%");

                if (string.IsNullOrEmpty(film.duration_minutes))
                {
                    cmd.Parameters.Add(new NpgsqlParameter("@minutes", NpgsqlDbType.Integer) { Value = DBNull.Value });
                }
                else
                {
                    cmd.Parameters.Add(new NpgsqlParameter("@minutes", NpgsqlDbType.Integer) 
                    { Value = int.Parse(film.duration_minutes) });
                }

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        filmT.Add(new Film
                        {
                            title = reader.GetString(0),
                            lead_actor = reader.GetString(1),
                            genre = reader.GetString(2),
                            duration_minutes = reader.IsDBNull(3) ? "N/A" : reader.GetInt32(3).ToString()
                        });
                    }
                }
            }
        }

        return filmT;
    }
}
