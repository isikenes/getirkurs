namespace Infrastructure.Entities
{
    public class PaymentMethod
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string CardNumber { get; set; }
        public string ExpiryDate { get; set; }
        public string CVC { get; set; }
    }
}
