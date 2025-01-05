using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSalesAPI.Application.Feautures.Commands.Order.CreateOrder
{
    public class CreateOrderCommandResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public Guid OrderNumber { get; set; }
        public string PaymentId { get; set; }
    }
}