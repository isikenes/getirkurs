namespace Business.DTOs
{
    public class PaymentMethodDTO
    {
        public string CardNumber { get; set; }
        public string ExpiryDate { get; set; }
        public string CVC { get; set; }
    }
}
