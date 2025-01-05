using CourseSalesAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSalesAPI.Domain.Entities
{
    public class OrderItem : BaseEntity
    {
        public Guid OrderId { get; set; }
        public Order Order { get; set; }

        public Guid CourseId { get; set; }
        public string CourseName { get; set; }
        public Course Course { get; set; }

        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}