using Infrastructure.Entities;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IPaymentMethodRepository
    {
        Task AddPaymentMethod(PaymentMethod paymentMethod);
        Task<PaymentMethod> GetPaymentMethodByUserId(string userId);
    }
}
