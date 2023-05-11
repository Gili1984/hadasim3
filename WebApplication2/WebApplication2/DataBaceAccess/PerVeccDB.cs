using System.Data.SqlClient;
using WebApplication2.Models;

namespace WebApplication2.DataBaceAccess
{
    public class PerVeccDB
    {
        private readonly string connectionString;

        public PerVeccDB()
        {
            this.connectionString = "Data Source=DESKTOP-OP9T8AN\\SQLEXPRESS;Initial Catalog=tartwo;Integrated Security=True";
        }

        public List<PerVecc> GetPerVecc()
        {
            List<PerVecc> PerVeccs = new List<PerVecc>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM PerVecc", connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int VaccId = (int)reader["VaccId"];
                    int id = (int)reader["Id"];

                    DateTime DateGet = (DateTime)reader["DateGet"];


                    PerVecc PerVecc = new PerVecc
                    {
                        VaccId = VaccId,
                        Id = id,
                        DateGet = DateGet,
                    };

                    PerVeccs.Add(PerVecc);
                }

                reader.Close();
                connection.Close();
            }

            return PerVeccs;
        }
        public PerVecc GetPerVeccById(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM PerVecc WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    int VaccId = (int)reader["VaccId"];
                    DateTime DateGet = (DateTime)reader["DateGet"];

                    PerVecc PerVecc = new PerVecc
                    {
                        Id = id,
                        VaccId = VaccId,
                        DateGet = DateGet
                    };

                    reader.Close();
                    connection.Close();

                    return PerVecc;
                }
                else
                {
                    reader.Close();
                    connection.Close();

                    return null;
                }
            }
        }

        public void AddPerVecc(PerVecc perVecc)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO PerVecc (VaccId, Id, DateGet) VALUES (@vaccId, @id, @dateGet)";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@vaccId", perVecc.VaccId);
                command.Parameters.AddWithValue("@id", perVecc.Id);
                command.Parameters.AddWithValue("@dateGet", perVecc.DateGet);

                int rowsAffected = command.ExecuteNonQuery();

                connection.Close();
            }
        }
    }
}
