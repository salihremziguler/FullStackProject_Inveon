using CourseSalesAPI.Application.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSalesAPI.Application.Feautures.Commands.Course.UpdateCourse
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateCourseCommandRequest, UpdateCourseCommandResponse>
    {
        readonly ICourseReadRepository _productReadRepository;
        readonly ICourseWriteRepository _productWriteRepository;
        readonly ILogger<UpdateProductCommandHandler> _logger;

        public UpdateProductCommandHandler(ICourseReadRepository productReadRepository, ICourseWriteRepository productWriteRepository, ILogger<UpdateProductCommandHandler> logger)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
            _logger = logger;
        }

        public async Task<UpdateCourseCommandResponse> Handle(UpdateCourseCommandRequest request, CancellationToken cancellationToken)
        {
            Domain.Entities.Course course = await _productReadRepository.GetByIdAsync(request.Id);
            course.Description = request.Description;
            course.Name = request.Name;
            course.Price = request.Price;
            course.Category = request.Category;
            await _productWriteRepository.SaveAsync();
            _logger.LogInformation("Product güncellendi...");
            return new();
        }
    }
}