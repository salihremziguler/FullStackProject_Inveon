using CourseSalesAPI.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSalesAPI.Application.Feautures.Queries.Course.GetAllCourse
{
    public class GetAllCourseQueryHandler : IRequestHandler<GetAllCourseQueryRequest, GetAllCourseQueryResponse>
    {
        readonly ICourseReadRepository _courseReadRepository;
        readonly ILogger<GetAllCourseQueryHandler> _logger;


        public GetAllCourseQueryHandler(ICourseReadRepository courseReadRepository, ILogger<GetAllCourseQueryHandler> logger)
        {
            _courseReadRepository = courseReadRepository;
            _logger = logger;
        }



        public async Task<GetAllCourseQueryResponse> Handle(GetAllCourseQueryRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get all courses");

            var totalCourseCount = _courseReadRepository.GetAll(false).Count();

            var courses = _courseReadRepository.GetAll(false).Skip(request.Page * request.Size).Take(request.Size)
                .Include(p => p.CourseImageFiles)
                .Select(p => new
                {
                    p.Id,
                    p.Name,
                    p.Description,
                    p.Price,
                    p.CreatedDate,
                    p.UpdatedDate,
                    p.Category,
                    p.CourseImageFiles
                }).ToList();


            return new()
            {
                Courses = courses,
                TotalCourseCount = totalCourseCount
            };


        }
    }
}
