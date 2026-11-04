namespace UniversityCafeteria.DTO
{
    public class ReviewViewModel
    {
        public List<CreateSupplyRequest> Items { get; set; } = new List<CreateSupplyRequest>();

        public double Subtotal { get; set; }

        public string Priority { get; set; } = "Normal";

        public double PreparationFee { get; set; }

        public double Discount { get; set; }

        public double FinalTotal { get; set; }
    }
}
