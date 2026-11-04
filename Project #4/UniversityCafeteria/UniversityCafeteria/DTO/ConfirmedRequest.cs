namespace UniversityCafeteria.DTO
{
    public class ConfirmedRequest
    {
        public int Id { get; set; }

        public int NumberOfProducts { get; set; }

        public double Subtotal { get; set; }

        public double PreparationFee { get; set; }

        public double Discount { get; set; }

        public double FinalTotal { get; set; }

        public string Status { get; set; } = "Confirmed";
    }
}
