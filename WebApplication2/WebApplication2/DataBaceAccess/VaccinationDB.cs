using System.Data.SqlClient;
using System.Xml.Linq;
using WebApplication2.Models;

namespace WebApplication2.DataBaceAccess
{
    public class VaccinationDB
    {
        private readonly string connectionString;

        public VaccinationDB()
        {
            this.connectionString = "Data Source=DESKTOP-OP9T8AN\\SQLEXPRESS;Initial Catalog=tartwo;Integrated Security=True";
        }

        public List<Vaccination> GetVaccination()
        {
            List<Vaccination> Vaccinations = new List<Vaccination>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM Vaccination", connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int VaccId = (int)reader["VaccId"];

                    string manufacturer = (string)reader["manufacturer"];


                    Vaccination Vaccination = new Vaccination
                    {
                        VaccId = VaccId,
                        manufacturer = manufacturer
                    };

                    Vaccinations.Add(Vaccination);
                }

                reader.Close();
                connection.Close();
            }

            return Vaccinations;
        }
        public Vaccination GetVaccinationById(int vaccinationId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Vaccination WHERE VaccId = @vaccId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@vaccId", vaccinationId);

                SqlDataReader reader = command.ExecuteReader();

                Vaccination vaccination = null;

                if (reader.Read())
                {
                    int VaccId = (int)reader["VaccId"];
                    string manufacturer = (string)reader["manufacturer"];

                    vaccination = new Vaccination
                    {
                        VaccId = VaccId,
                        manufacturer = manufacturer
                    };
                }

                reader.Close();
                connection.Close();

                return vaccination;
            }
        }

        public void AddVaccination(Vaccination Vaccination)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO Vaccination (VaccId,manufacturer) VALUES (@vaccId, @manufacturer)";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@vaccId", Vaccination.VaccId);
                command.Parameters.AddWithValue("@manufacturer", Vaccination.manufacturer);


                int rowsAffected = command.ExecuteNonQuery();

                connection.Close();
            }
        }
    }
}
