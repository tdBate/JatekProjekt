using MySql.Data.MySqlClient;

namespace JatekProjekt;

public class AdatbazisKezelo
{
    private readonly string connectionString;


    public AdatbazisKezelo()
    {
        connectionString =
            "server=localhost;database=JatekDb;uid=root;pwd=;"; // Irjuk meg a connectionstringet a megfelelő adatbázisra hivatkozva
    }


    /// Beolvassa az összes karaktert az adatbázisból egy List
    /// <Karakter>-be.
    public List<Karakter> AdatokBetoltese()
    {
        var karakterek = new List<Karakter>();

        using (var connection = new MySqlConnection(connectionString))
        {
            var query = "SELECT * from Karakterek";

            using (var command = new MySqlCommand(query, connection))
            {
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var karakter = new Karakter(
                            Convert.ToInt32(reader["id"].ToString()),
                            reader["nev"].ToString(),
                            reader["osztaly"].ToString(),
                            Convert.ToInt32(reader["szint"].ToString()),
                            Convert.ToInt32(reader["arany"].ToString()),
                            reader["szerver"].ToString()
                        );

                        karakterek.Add(karakter);
                    }
                }
            }
        }

        return karakterek;
    }
}