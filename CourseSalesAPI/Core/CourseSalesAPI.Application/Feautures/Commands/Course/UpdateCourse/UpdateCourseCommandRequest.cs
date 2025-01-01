using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSalesAPI.Application.Feautures.Commands.Course.UpdateCourse
{
    public class UpdateCourseCommandRequest : IRequest<UpdateCourseCommandResponse>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}