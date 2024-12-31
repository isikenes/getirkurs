using Business.DTOs;

namespace Business.Interfaces
{
    public interface IPaymentMethodService
    {
        Task AddPaymentMethod(PaymentMethodDTO paymentMethod, string userId);
        Task<PaymentMethodDTO> GetPaymentMethodByUserId(string userId);
    }
}
