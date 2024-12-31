using Business.DTOs;
using Business.Interfaces;
using Infrastructure.Entities;
using Infrastructure.UnitOfWork;
using System.Text;

namespace Business.Services
{
    public class PaymentMethodService(IUnitOfWork unitOfWork) : IPaymentMethodService
    {
        public async Task AddPaymentMethod(PaymentMethodDTO paymentMethod, string userId)
        {
            var paymentMethodEntity = new PaymentMethod
            {
                UserId = userId,
                CardNumber = Encrypt(paymentMethod.CardNumber),
                ExpiryDate = Encrypt(paymentMethod.ExpiryDate),
                CVC = Encrypt(paymentMethod.CVC)
            };

            await unitOfWork.PaymentMethods.AddPaymentMethod(paymentMethodEntity);
            await unitOfWork.Save();
        }

        public async Task<PaymentMethodDTO> GetPaymentMethodByUserId(string userId)
        {
            var paymentMethod = await unitOfWork.PaymentMethods.GetPaymentMethodByUserId(userId);
            if (paymentMethod == null) throw new Exception("Payment method not found");

            var decrypted = Decrypt(paymentMethod.CardNumber);

            return new PaymentMethodDTO
            {
                CardNumber = $"**** **** **** {decrypted[^4..]}",
                ExpiryDate = Decrypt(paymentMethod.ExpiryDate),
                CVC = Decrypt(paymentMethod.CVC)
            };

        }

        private string Encrypt(string value)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(value));
        }

        private string Decrypt(string value)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(value));
        }
    }
}
