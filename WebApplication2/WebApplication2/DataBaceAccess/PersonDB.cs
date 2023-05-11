using System.Data.SqlClient;
using WebApplication2.Models;
namespace WebApplication2.DataBaceAccess;


public class PersonDB
{
    private readonly string connectionString;
   
    public PersonDB()
    {
        this.connectionString = "Data Source=DESKTOP-OP9T8AN\\SQLEXPRESS;Initial Catalog=tartwo;Integrated Security=True";
    }

    public List<Person> GetPeople()
    {
        List<Person> people = new List<Person>();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM persons ", connection);
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                int id = (int)reader["id"];
                string name = (string)reader["name"];
                string adress = (string)reader["adress"];
                DateTime birthDate = (DateTime)reader["birthdate"];
                string phone = (string)reader["phone"];
                string selfPhone = (string)reader["selfPhone"];
                DateTime? positiveDate = reader.IsDBNull(reader.GetOrdinal("positiveDate")) ? (DateTime?)null : (DateTime?)reader["PositiveDate"];
                DateTime? healthyDate = reader.IsDBNull(reader.GetOrdinal("healthyDate")) ? (DateTime?)null : (DateTime?)reader["HealthyDate"];

                Person person = new Person
                {
                    Id = id,
                    Name = name,
                    Address = adress,
                    BirthDate = birthDate,
                    Phone = phone,
                    SelfPhone = selfPhone,
                    PositiveDate = positiveDate,
                    HealthyDate = healthyDate
                };

                people.Add(person);
            }

            reader.Close();
            connection.Close();
        }

        return people;
    }

    public Person GetPersonById(int id)
    {
        Person person = null;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM persons WHERE Id = @Id", connection);
            command.Parameters.AddWithValue("@Id", id);

            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                string name = (string)reader["Name"];
                string address = (string)reader["Adress"];
                DateTime birthDate = (DateTime)reader["BirthDate"];
                string phone = (string)reader["Phone"];
                string selfPhone = (string)reader["SelfPhone"];
                DateTime? positiveDate = reader.IsDBNull(reader.GetOrdinal("PositiveDate")) ? (DateTime?)null : (DateTime?)reader["PositiveDate"];
                DateTime? healthyDate = reader.IsDBNull(reader.GetOrdinal("HealthyDate")) ? (DateTime?)null : (DateTime?)reader["HealthyDate"];

                person = new Person
                {
                    Id = id,
                    Name = name,
                    Address = address,
                    BirthDate = birthDate,
                    Phone = phone,
                    SelfPhone = selfPhone,
                    PositiveDate = positiveDate,
                    HealthyDate = healthyDate
                };
            }

            reader.Close();
            connection.Close();
        }

        return person;
    }

    public int AddPerson(Person person)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            SqlCommand command = new SqlCommand("INSERT INTO persons (Name, Adress, BirthDate, Phone, SelfPhone, PositiveDate, HealthyDate) " +
                                                "VALUES (@Name, @Adress, @BirthDate, @Phone, @SelfPhone, @PositiveDate, @HealthyDate); " +
                                                "SELECT SCOPE_IDENTITY();", connection);

            command.Parameters.AddWithValue("@Name", person.Name);
            command.Parameters.AddWithValue("@Adress", person.Address);
            command.Parameters.AddWithValue("@BirthDate", person.BirthDate);
            command.Parameters.AddWithValue("@Phone", person.Phone);
            command.Parameters.AddWithValue("@SelfPhone", person.SelfPhone);
            command.Parameters.AddWithValue("@PositiveDate", person.PositiveDate ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@HealthyDate", person.HealthyDate ?? (object)DBNull.Value);

            int newId = Convert.ToInt32(command.ExecuteScalar());

            connection.Close();

            return newId;
        }
    }

}

