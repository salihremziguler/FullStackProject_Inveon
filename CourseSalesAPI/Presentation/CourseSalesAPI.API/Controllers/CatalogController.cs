using CourseSalesAPI.Application.Feautures.Commands.Course.CreateCourse;
using CourseSalesAPI.Application.Feautures.Queries.Course.GetAllCourse;
using CourseSalesAPI.Application.Repositories;
using CourseSalesAPI.Application.RequestParameters;
using CourseSalesAPI.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CourseSalesAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly ICourseReadRepository _courseReadRepository;
        private readonly ICourseWriteRepository _courseWriteRepository;
        private readonly IMediator _mediator;


        public CatalogController(ICourseReadRepository courseReadRepository, ICourseWriteRepository courseWriteRepository, IMediator mediator)
        {
            _courseReadRepository = courseReadRepository;
            _courseWriteRepository = courseWriteRepository;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllCourseQueryRequest getAllCourseQueryRequest)
        {

       GetAllCourseQueryResponse response=   await  _mediator.Send(getAllCourseQueryRequest); 
        return Ok(response);



            /*await _courseWriteRepository.AddRangeAsync(new()
            {
                new(){Id=Guid.NewGuid(),Category="a",CreatedDate=DateTime.Now,Description="B",Name="C",Price=13 } });

            await _courseWriteRepository.SaveAsync();*/
        }


        [HttpPost]
        public async Task<IActionResult> Post(CreateCourseCommandRequest createCourseCommandRequest)
        {
         CreateCourseCommandResponse response=await   _mediator.Send(createCourseCommandRequest);
            return StatusCode((int)HttpStatusCode.Created);
        }



        [HttpGet("GetCourses")] // Endpoint: api/Catalog/GetCourses
        public async  Task<IActionResult> GetCourses([FromQuery] GetAllCourseQueryRequest getAllCourseQueryRequest)
        {
            GetAllCourseQueryResponse response = await _mediator.Send(getAllCourseQueryRequest);
            return Ok(response);



           /* var courses = _courseReadRepository.GetAll();
            return Ok(courses);*/
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

