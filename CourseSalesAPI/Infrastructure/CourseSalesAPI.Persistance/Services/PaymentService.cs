using CourseSalesAPI.Application.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSalesAPI.Persistance.Services
{
    public class PaymentService : IPaymentService
    {
        public async Task<string> ProcessPaymentAsync(decimal totalPrice)
        {
            // Gerçek bir ödeme entegrasyonu (Stripe, PayPal, İyzico vs.) burada yapılır.
            // Burada mock'lanmış bir işlem yapıyoruz:
            if (totalPrice <= 0)
                throw new Exception("Geçersiz ödeme tutarı!");

            await Task.Delay(500); // Sahte bekleme

            // Ödemenin başarılı olduğunu varsayıp rastgele bir PaymentId döndürüyoruz
            return Guid.NewGuid().ToString();
        }
    }
}