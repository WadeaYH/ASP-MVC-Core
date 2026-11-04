namespace UniversityCafeteria.DTO
{
    public class CreateSupplyRequest
    {
        public String ProductName { get; set; }

        public String Category { get; set; }

        public int Quantity { get; set; }

        public double UnitPrice { get; set; }

        public string Priority { get; set; }

        public double Total { get; set; }

        public CreateSupplyRequest()
        {

        }

        public CreateSupplyRequest(string productName, string category, int quantity, double unitPrice, string priority)
        {
            ProductName = productName;
            Category = category;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Priority = priority;
        }
    }
}
