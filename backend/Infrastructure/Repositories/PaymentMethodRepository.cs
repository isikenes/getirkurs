using Infrastructure.Database;
using Infrastructure.Entities;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PaymentMethodRepository(AppDbContext context) : IPaymentMethodRepository
    {
        public async Task AddPaymentMethod(PaymentMethod paymentMethod)
        {
            await context.PaymentMethods.AddAsync(paymentMethod);
        }

        public Task<PaymentMethod> GetPaymentMethodByUserId(string userId)
        {
            return context.PaymentMethods.FirstOrDefaultAsync(pm => pm.UserId == userId);
        }
    }
}
