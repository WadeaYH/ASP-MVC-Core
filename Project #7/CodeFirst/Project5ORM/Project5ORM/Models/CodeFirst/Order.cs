namespace Project5ORM.Models.CodeFirst
{
    public class Order
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public DateOnly OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }
    }
}
