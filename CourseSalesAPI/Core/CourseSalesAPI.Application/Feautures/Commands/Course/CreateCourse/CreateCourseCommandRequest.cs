using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSalesAPI.Application.Feautures.Commands.Course.CreateCourse
{
    public class CreateCourseCommandRequest : IRequest<CreateCourseCommandResponse>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
      
        public string Category { get; set; }

    }
}
