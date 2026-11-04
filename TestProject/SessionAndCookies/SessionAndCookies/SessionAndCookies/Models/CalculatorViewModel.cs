namespace SessionAndCookies.Models
{
    public class CalculatorViewModel
    {
        public int Number1 { get; set; }
        public int Number2 { get; set; }
        public string? Operation { get; set; }
        public decimal? Result { get; set; }
        public string? Error { get; set; }
    }
}
