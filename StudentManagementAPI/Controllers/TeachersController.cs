using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagementAPI.Models;
using StudentManagementAPI.Models.DTO;
using StudentManagementAPI.Repositories.Interface;

namespace TeacherManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly ITeacherRepository teacherRepository;

        public TeachersController(ITeacherRepository teacherRepository)
        {
            this.teacherRepository = teacherRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTeacher([FromBody] CreateTeacherRequestDto request)
        {


            //Map DTO to Domain Model


            var teacher = new Teacher
            {
                FirstName= request.FirstName,
                LastName= request.LastName,
                ContactNo= request.ContactNo,
                Email= request.Email,

            };

            await teacherRepository.CreateAsync(teacher);

            //Map Domain model to DTO

            var response = new TeacherDto
            {
                TeacherId = teacher.TeacherId,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                ContactNo = teacher.ContactNo,
                Email = teacher.Email,
               
            };

            return Ok(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllTeachers()
        {
            var teachers = await teacherRepository.GetAllAsync();

            //Map Domain model to DTO

            var response = new List<TeacherDto>();

            foreach (var teacher in teachers)
            {
                response.Add(new TeacherDto
                {
                    
                    TeacherId = teacher.TeacherId,
                    FirstName = teacher.FirstName,
                    LastName = teacher.LastName,
                    ContactNo = teacher.ContactNo,
                    Email = teacher.Email,
                });
            }

            return Ok(response);
        }
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetTeacherById([FromRoute] int id)
        {
            var existingTeacher = await teacherRepository.GetById(id);

            //Map Domain model to DTO
            if (existingTeacher is null)
            {
                return NotFound();
            }
            var response = new TeacherDto
            {

                TeacherId = existingTeacher.TeacherId,
                FirstName = existingTeacher.FirstName,
                LastName = existingTeacher.LastName,
                ContactNo = existingTeacher.ContactNo,
                Email = existingTeacher.Email,
            };
               

                
            return Ok(response);
        }

        //PUT:https://localhost:7151/api/Categories/{id}
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> EditTeacher([FromRoute] int id, UpdateTeacherRequestDto request)
        {
            //category DTO to Domain Model
            var teacher = new Teacher
            {
                TeacherId = id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                
                ContactNo = request.ContactNo,
                Email = request.Email,
                
            };

            teacher = await teacherRepository.UpdateAsync(teacher);

            if (teacher == null)
            {
                return NotFound();
            }
            //Convert Domain model to DTO
            var response = new TeacherDto
            {
                TeacherId = teacher.TeacherId,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                
                ContactNo = teacher.ContactNo,
                Email = teacher.Email,
                
            };
            return Ok(response);
        }

        //DELETE:https://localhost:7151/api/Categories/{id}
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] int id)
        {
            var teacher = await teacherRepository.DeleteAsync(id);
            if (teacher is null)
            {
                return NotFound();
            }

            //convert Domain model to DTO
            var response = new TeacherDto
            {
                TeacherId = teacher.TeacherId,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
               
                ContactNo = teacher.ContactNo,
                Email = teacher.Email,
                
            };
            return Ok(response);
        }
    }


}
