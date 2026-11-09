namespace Project5ORM.Models.CodeFirst
{
    public class Employee
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
        public decimal Salary { get; set; }
        public DateOnly HireDate { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();


    }
}
