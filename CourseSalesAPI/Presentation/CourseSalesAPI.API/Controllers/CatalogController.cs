using CourseSalesAPI.Application.Repositories;
using CourseSalesAPI.Application.RequestParameters;
using CourseSalesAPI.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseSalesAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly ICourseReadRepository _courseReadRepository;
        private readonly ICourseWriteRepository _courseWriteRepository;

        public CatalogController(ICourseReadRepository courseReadRepository, ICourseWriteRepository courseWriteRepository)
        {
            _courseReadRepository = courseReadRepository;
            _courseWriteRepository = courseWriteRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var courses= _courseReadRepository.GetAll();
            return Ok(courses);


            /*await _courseWriteRepository.AddRangeAsync(new()
            {
                new(){Id=Guid.NewGuid(),Category="a",CreatedDate=DateTime.Now,Description="B",Name="C",Price=13 } });

            await _courseWriteRepository.SaveAsync();*/
        }

        [HttpGet("GetCourses")] // Endpoint: api/Catalog/GetCourses
        public IActionResult GetCourses()
        {
            var courses = _courseReadRepository.GetAll();
            return Ok(courses);
        }

        [HttpGet("GetCourseById/{id}")]
        public async Task<IActionResult> GetCourseById(string id)
        {
            var product= await _courseReadRepository.GetByIdAsync(id);
            return Ok(product);

        }


        [HttpPost("AddCourse")]
        public async Task<IActionResult> AddCourse([FromBody] Course request)
        {
            if (request == null)
            {
                return BadRequest("Invalid course data.");
            }

            var newCourse = new Course
            {
               
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                Category = request.Category,
              
            };

            await _courseWriteRepository.AddAsync(newCourse);
            await _courseWriteRepository.SaveAsync();

            return Ok(newCourse);
        }
    }



}

