using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSalesAPI.Application.DTOs
{
    public class CreateOrderItem
    {
        public string CourseId { get; set; } // Kurs ID'si
        public string CourseName { get; set; } // Kurs adı
        public int Quantity { get; set; } // Miktar
        public decimal UnitPrice { get; set; } // Birim fiyat
    }
}