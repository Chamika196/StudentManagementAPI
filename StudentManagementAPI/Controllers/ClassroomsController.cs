using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagementAPI.Models.DTO;
using StudentManagementAPI.Models;
using StudentManagementAPI.Repositories.Implementation;
using StudentManagementAPI.Repositories.Interface;
using Azure.Core;

namespace StudentManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassroomsController : ControllerBase
    {
        private readonly IClassroomRepository classroomRepository;

        public ClassroomsController(IClassroomRepository classroomRepository)
        {
            this.classroomRepository = classroomRepository;
        }
        [HttpPost]
        public async Task<IActionResult> CreateClassroom([FromBody] CreateClassroomRequestDto request)
        {


            //Map DTO to Domain Model


            var classroom = new Classroom
            {
               ClassName= request.ClassName,
               
            };

            await classroomRepository.CreateAsync(classroom);

            //Map Domain model to DTO

            var response = new ClassroomDto
            {
                ClassroomId=classroom.ClassroomId,
                ClassName=classroom.ClassName,
            };

            return Ok(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllClassrooms()
        {
            var classrooms = await classroomRepository.GetAllAsync();

            //Map Domain model to DTO

            var response = new List<ClassroomDto>();

            foreach (var classroom in classrooms)
            {
                response.Add(new ClassroomDto
                {
                    ClassroomId = classroom.ClassroomId,
                    ClassName = classroom.ClassName,
                });
            }

            return Ok(response);
        }
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetClassroomById([FromRoute] int id)
        {
            var existingClassroom = await classroomRepository.GetById(id);

            //Map Domain model to DTO
            if (existingClassroom is null)
            {
                return NotFound();
            }
            var response = new ClassroomDto
            {
                ClassroomId = existingClassroom.ClassroomId,
                ClassName = existingClassroom.ClassName,
            };


            return Ok(response);
        }

        //PUT:https://localhost:7151/api/Categories/{id}
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> EditClassroom([FromRoute] int id, UpdateClassroomRequestDto request)
        {
            //category DTO to Domain Model
            var classroom = new Classroom
            {
                ClassroomId = id,
                ClassName = request.ClassName,
            };

            classroom = await classroomRepository.UpdateAsync(classroom);

            if (classroom == null)
            {
                return NotFound();
            }
            //Convert Domain model to DTO
            var response = new ClassroomDto
            {
                ClassroomId = id,
                ClassName = request.ClassName,
            };
            return Ok(response);
        }

        //DELETE:https://localhost:7151/api/Categories/{id}
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] int id)
        {
            var classroom = await classroomRepository.DeleteAsync(id);
            if (classroom is null)
            {
                return NotFound();
            }

            //convert Domain model to DTO
            var response = new ClassroomDto
            {
                ClassroomId = classroom.ClassroomId,
                ClassName = classroom.ClassName,
            };
            return Ok(response);
        }
    }
}
