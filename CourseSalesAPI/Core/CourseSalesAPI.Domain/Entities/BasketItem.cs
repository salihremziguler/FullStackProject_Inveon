using CourseSalesAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSalesAPI.Domain.Entities
{
    public class BasketItem : BaseEntity
    {
        public Guid BasketId { get; set; }
        public Guid CourseId { get; set; }

        public int Quantity { get; set; }

        public Basket Basket { get; set; }
        public Course Course { get; set; }
    }
}