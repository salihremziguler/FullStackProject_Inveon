using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSalesAPI.Application.ViewModels.Paymet
{
    public class VM_PaymentResponse
    {
        public string ClientSecret { get; set; }
        public string PaymentIntentId { get; set; }
        public decimal TotalAmount { get; set; }
       // public Guid orderId;
    }
}