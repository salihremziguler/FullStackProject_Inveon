using CourseSalesAPI.Application.DTOs;
using CourseSalesAPI.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSalesAPI.Application.Feautures.Commands.Order.CreateOrder
{

    public class CreateOrderCommandRequest : IRequest<CreateOrderCommandResponse>
    {
        public string UserId { get; set; } // Kullanıcı ID'si
        public string PaymentId { get; set; } // Ödeme sağlayıcısından gelen ödeme ID'si
        public decimal TotalPrice { get; set; } // Siparişin toplam tutarı
        public string ShippingAddress { get; set; } // Gönderim adresi
        public string BillingAddress { get; set; } // Fatura adresi

        // Sipariş öğelerinin listesi
        public List<CreateOrderItem> Items { get; set; }
    }
    }