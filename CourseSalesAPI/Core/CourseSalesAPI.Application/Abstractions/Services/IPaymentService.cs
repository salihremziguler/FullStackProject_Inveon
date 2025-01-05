using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSalesAPI.Application.Abstractions.Services
{
    public interface IPaymentService
    {
        Task<string> ProcessPaymentAsync(decimal totalPrice);
    }
}
