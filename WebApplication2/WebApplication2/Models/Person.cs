namespace WebApplication2.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime BirthDate { get; set; }
        public string Phone { get; set; }
        public string SelfPhone { get; set; }
        public DateTime? PositiveDate { get; set; }
        public DateTime? HealthyDate { get; set; }
    }
}
