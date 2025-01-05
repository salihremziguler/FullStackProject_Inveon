using CourseSalesAPI.Application.Repositories;
using CourseSalesAPI.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C = CourseSalesAPI.Domain.Entities;

namespace CourseSalesAPI.Application.Feautures.Queries.Course.GetByIdCourse
{
    internal class GetByIdCourseQueryHandler : IRequestHandler<GetByIdCourseQueryRequest, GetByIdCourseQueryResponse>
    {

        readonly ICourseReadRepository _courseReadRepository;
        public GetByIdCourseQueryHandler(ICourseReadRepository courseReadRepository)
        {
            _courseReadRepository = courseReadRepository;
        }

        public async Task<GetByIdCourseQueryResponse> Handle(GetByIdCourseQueryRequest request, CancellationToken cancellationToken)
        {
            C.Course course = await _courseReadRepository.GetByIdAsync(request.Id, false);
            return new()
            {
                Name = course.Name,
                Price = course.Price,
                Description = course.Description,
                Category = course.Category,
                Image=course.Image,

            };
        }
    }
}
