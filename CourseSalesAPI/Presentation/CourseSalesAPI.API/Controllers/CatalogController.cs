using CourseSalesAPI.Application.Feautures.Commands.Course.CreateCourse;
using CourseSalesAPI.Application.Feautures.Commands.Course.RemoveCourse;
using CourseSalesAPI.Application.Feautures.Commands.Course.UpdateCourse;

using CourseSalesAPI.Application.Feautures.Queries.Course.GetAllCourse;
using CourseSalesAPI.Application.Feautures.Queries.Course.GetByIdCourse;

using CourseSalesAPI.Application.Repositories;
using CourseSalesAPI.Application.RequestParameters;
using CourseSalesAPI.Application.ViewModels;
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

        private readonly IMediator _mediator;

        public CatalogController(ICourseReadRepository courseReadRepository, IMediator mediator)
        {
            _courseReadRepository = courseReadRepository;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllCourseQueryRequest getAllCourseQueryRequest)
        {

            GetAllCourseQueryResponse response = await _mediator.Send(getAllCourseQueryRequest);
            return Ok(response);

        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Get([FromRoute] GetByIdCourseQueryRequest getByIdCourseQueryRequest)
        {
            GetByIdCourseQueryResponse response = await _mediator.Send(getByIdCourseQueryRequest);
            return Ok(response);
        }




        [HttpPost("AddCourseWithImage")]
        public async Task<IActionResult> Post(CreateCourseCommandRequest createCourseCommandRequest)
        {
            CreateCourseCommandResponse response = await _mediator.Send(createCourseCommandRequest);
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateCourseCommandRequest updateCourseCommandRequest)
        {
            UpdateCourseCommandResponse response = await _mediator.Send(updateCourseCommandRequest);
            return Ok();
        }


        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] RemoveCourseCommandRequest removeCourseCommandRequest)
        {
            RemoveCourseCommandResponse response = await _mediator.Send(removeCourseCommandRequest);
            return Ok();
        }




        [HttpGet("GetCourses")]
        public async Task<IActionResult> GetCourses([FromQuery] GetAllCourseQueryRequest getAllCourseQueryRequest)
        {
            GetAllCourseQueryResponse response = await _mediator.Send(getAllCourseQueryRequest);
            return Ok(response);


            
        }

        [HttpGet("GetCourseById/{id}")]
        public async Task<IActionResult> GetCourseById(string id)
        {
            var product = await _courseReadRepository.GetByIdAsync(id);
            return Ok(product);

        }



    }
}





