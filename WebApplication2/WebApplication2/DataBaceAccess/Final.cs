using System.Data.SqlClient;
using WebApplication2.Models;

namespace WebApplication2.DataBaceAccess
{
    public class Final
    {
        private readonly string connectionString;

        public Final()
        {
            this.connectionString = "Data Source=DESKTOP-OP9T8AN\\SQLEXPRESS;Initial Catalog=tartwo;Integrated Security=True";
        }
        public List<(Person, Vaccination, PerVecc)> GetPersonVaccinationJoin()
        {
            List<(Person, Vaccination, PerVecc)> result = new List<(Person, Vaccination, PerVecc)>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(@"SELECT p.Id, p.Name, p.Adress, p.BirthDate, p.Phone, p.SelfPhone, p.PositiveDate, p.HealthyDate,
                                              pv.VaccId, v.manufacturer, pv.DateGet
                                              FROM Persons p
                                              INNER JOIN PerVecc pv ON p.Id = pv.Id
                                              INNER JOIN Vaccination v ON pv.VaccId = v.VaccId", connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Person person = new Person
                    {
                        Id = (int)reader["Id"],
                        Name = (string)reader["Name"],
                        Address = (string)reader["Adress"],
                        BirthDate = (DateTime)reader["BirthDate"],
                        Phone = (string)reader["Phone"],
                        SelfPhone = (string)reader["SelfPhone"],
                        PositiveDate = reader.IsDBNull(reader.GetOrdinal("PositiveDate")) ? (DateTime?)null : (DateTime?)reader["PositiveDate"],
                        HealthyDate = reader.IsDBNull(reader.GetOrdinal("HealthyDate")) ? (DateTime?)null : (DateTime?)reader["HealthyDate"]
                    };

                    Vaccination vaccination = new Vaccination
                    {
                        VaccId = (int)reader["VaccId"],
                        manufacturer = (string)reader["manufacturer"]
                    };

                    PerVecc perVecc = new PerVecc
                    {
                        Id = (int)reader["Id"],
                        VaccId = (int)reader["VaccId"],
                        DateGet = (DateTime)reader["DateGet"]
                    };

                    result.Add((person, vaccination, perVecc));
                }

                reader.Close();
                connection.Close();
            }

            return result;
        }
        public List<(Person, Vaccination, PerVecc)> GetById(int id)
        {
            List<(Person, Vaccination, PerVecc)> final = new List<(Person, Vaccination, PerVecc)>();


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(
                    "SELECT P.Id, P.Name, P.Adress, P.BirthDate, P.Phone, P.SelfPhone, " +
                    "P.PositiveDate, P.HealthyDate, PV.VaccId, PV.DateGet, V.manufacturer " +
                    "FROM Persons AS P " +
                    "INNER JOIN PerVecc AS PV ON P.Id = PV.Id " +
                    "INNER JOIN Vaccination AS V ON PV.VaccId = V.VaccId " +
                    "WHERE P.Id = @id", connection);

                command.Parameters.AddWithValue("@id", id);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Person person = new Person
                    {
                        Id = (int)reader["Id"],
                        Name = (string)reader["Name"],
                        Address = (string)reader["Adress"],
                        BirthDate = (DateTime)reader["BirthDate"],
                        Phone = (string)reader["Phone"],
                        SelfPhone = (string)reader["SelfPhone"],
                        PositiveDate = reader.IsDBNull(reader.GetOrdinal("PositiveDate")) ? (DateTime?)null : (DateTime?)reader["PositiveDate"],
                        HealthyDate = reader.IsDBNull(reader.GetOrdinal("HealthyDate")) ? (DateTime?)null : (DateTime?)reader["HealthyDate"]
                    };

                    Vaccination vaccination = new Vaccination
                    {
                        VaccId = (int)reader["VaccId"],
                        manufacturer = (string)reader["manufacturer"]
                    };

                    PerVecc perVecc = new PerVecc
                    {
                        Id = (int)reader["Id"],
                        VaccId = (int)reader["VaccId"],
                        DateGet = (DateTime)reader["DateGet"]
                    };

                    final.Add((person, vaccination, perVecc));
                }

                reader.Close();
                connection.Close();
            }

            return final;

        }
        public void Add(Person person, PerVecc perVecc, Vaccination vaccination)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    SqlCommand command = new SqlCommand("INSERT INTO Persons (Name, Address, BirthDate, Phone, SelfPhone, PositiveDate, HealthyDate) " +
                                                        "VALUES (@Name, @Adress, @BirthDate, @Phone, @SelfPhone, @PositiveDate, @HealthyDate);" +
                                                        "SELECT SCOPE_IDENTITY();", connection, transaction);

                    command.Parameters.AddWithValue("@Name", person.Name);
                    command.Parameters.AddWithValue("@Adress", person.Address);
                    command.Parameters.AddWithValue("@BirthDate", person.BirthDate);
                    command.Parameters.AddWithValue("@Phone", person.Phone);
                    command.Parameters.AddWithValue("@SelfPhone", person.SelfPhone);
                    command.Parameters.AddWithValue("@PositiveDate", (object)person.PositiveDate ?? DBNull.Value);
                    command.Parameters.AddWithValue("@HealthyDate", (object)person.HealthyDate ?? DBNull.Value);

                    int personId = Convert.ToInt32(command.ExecuteScalar());

                    command = new SqlCommand("INSERT INTO Vaccination (manufacturer) VALUES (@Manufacturer);" +
                                              "SELECT SCOPE_IDENTITY();", connection, transaction);

                    command.Parameters.AddWithValue("@Manufacturer", vaccination.manufacturer);

                    int vaccId = Convert.ToInt32(command.ExecuteScalar());

                    command = new SqlCommand("INSERT INTO PerVecc (Id, VaccId, DateGet) VALUES (@Id, @VaccId, @DateGet)", connection, transaction);

                    command.Parameters.AddWithValue("@Id", personId);
                    command.Parameters.AddWithValue("@VaccId", vaccId);
                    command.Parameters.AddWithValue("@DateGet", perVecc.DateGet);

                    command.ExecuteNonQuery();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

    }
    }
