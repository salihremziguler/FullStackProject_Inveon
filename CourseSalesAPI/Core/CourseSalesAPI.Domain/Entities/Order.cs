using CourseSalesAPI.Domain.Entities.Common;
using CourseSalesAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSalesAPI.Domain.Entities
{
    public class Order : BaseEntity
    {
       // public Guid OrderNumber { get; set; }
        public string PaymentId { get; set; }  // Ödeme sağlayıcısından dönen ID

        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }

        // Adres bilgileri:
       // public string ShippingAddress { get; set; }
       // public string BillingAddress { get; set; }

        // Sipariş veren kullanıcı
        public string UserId { get; set; }
        public AppUser User { get; set; }

        // OrderItems (1-N ilişkisi)
        public ICollection<OrderItem> OrderItems { get; set; }


       // public Basket Basket { get; set; }

       // public CompletedOrder CompletedOrder { get; set; }


      

    }
}
