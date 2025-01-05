using CourseSalesAPI.Domain.Entities.Common;
using CourseSalesAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSalesAPI.Domain.Entities
{
    public class PurchasedCourse:BaseEntity
    {
       
        public string UserId { get; set; } // Kullanıcı ID'si
        public Guid CourseId { get; set; } // Satın alınan kursun ID'si
        public DateTime PurchaseDate { get; set; } // Satın alma tarihi

        // İlişkiler
        public AppUser User { get; set; }
        public Course Course { get; set; }
    }
}