using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementAPI.Models;
using StudentManagementAPI.Models.DTO;
using StudentManagementAPI.Repositories.Implementation;
using StudentManagementAPI.Repositories.Interface;

namespace StudentManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentRepository studentRepository;

        public StudentsController(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }
        private static int CalculateAge(DateTime dateOfBirth)
        {
            DateTime currentDate = DateTime.Now;
            int age = currentDate.Year - dateOfBirth.Year;

            // Adjust age if the birthday hasn't occurred yet this year
            if (currentDate.Month < dateOfBirth.Month || (currentDate.Month == dateOfBirth.Month && currentDate.Day < dateOfBirth.Day))
            {
                age--;
            }

            return age;
        }
        [HttpPost]
        public async Task<IActionResult> CreateStudent([FromBody] CreateStudentRequestDto request)
        {


            //Map DTO to Domain Model
            

            var student = new Student
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                ContactPerson = request.ContactPerson,
                ContactNo = request.ContactNo,
                Email = request.Email,
                DateOfBirth = request.DateOfBirth.Date,
                Age = CalculateAge(request.DateOfBirth),
                ClassroomId = request.ClassroomId,
                Classroom = request.Classroom

            };

            await studentRepository.CreateAsync(student);

            //Map Domain model to DTO

            var response = new StudentDto
            {
                StudentId = student.StudentId,
                FirstName = student.FirstName,
                LastName = student.LastName,
                ContactPerson = student.ContactPerson,
                ContactNo = student.ContactNo,
                Email = student.Email,
                DateOfBirth = student.DateOfBirth,
                Age = student.Age,
                ClassroomId = student.ClassroomId,
                Classroom = student.Classroom
            };

            return Ok(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await studentRepository.GetAllAsync();

            if (students == null || !students.Any())
            {
                return Ok(new List<StudentDto>()); // Return an empty list if no students are found
            }

            // Map Domain model to DTO
            var response = new List<StudentDto>();

            foreach (var student in students)
            {
                response.Add(new StudentDto
                {
                    StudentId = student.StudentId,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    ContactPerson = student.ContactPerson,
                    ContactNo = student.ContactNo,
                    Email = student.Email,
                    DateOfBirth = (DateTime)(student.DateOfBirth != DateTime.MinValue ? student.DateOfBirth.Date : (DateTime?)null),
                    Age = student.Age.HasValue ? student.Age : 0,
                    ClassroomId = student.ClassroomId,
                    Classroom = student.Classroom
                });
            }

            return Ok(response);
        }





        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetStudentById([FromRoute] int id)
        {
            var existingStudent = await studentRepository.GetById(id);
            if (existingStudent == null)
            {
                return NotFound();
            }

            //Map Domain model to DTO
            if (existingStudent is null)
            {
                return NotFound();
            }
            var response = new StudentDto
            {
                StudentId = existingStudent.StudentId,
                FirstName = existingStudent.FirstName,
                LastName = existingStudent.LastName,
                ContactPerson = existingStudent.ContactPerson,
                ContactNo = existingStudent.ContactNo,
                Email = existingStudent.Email,
                DateOfBirth = existingStudent.DateOfBirth.Date,
                Age = existingStudent.Age,
                ClassroomId = existingStudent.ClassroomId,
                Classroom = existingStudent.Classroom
            };


            return Ok(response);
        }

        //PUT:https://localhost:7151/api/Categories/{id}
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> EditStudent([FromRoute] int id, UpdateStudentRequestDto request)
        {
            //category DTO to Domain Model
            var student = new Student
            {
                StudentId = id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                ContactPerson = request.ContactPerson,
                ContactNo = request.ContactNo,
                Email = request.Email,
                DateOfBirth = request.DateOfBirth.Date,
                Age = CalculateAge(request.DateOfBirth),
                ClassroomId = request.ClassroomId,
                Classroom = request.Classroom
            };

            student = await studentRepository.UpdateAsync(student);

            if (student == null)
            {
                return NotFound();
            }
            //Convert Domain model to DTO
            var response = new StudentDto
            {
                StudentId = student.StudentId,
                FirstName = student.FirstName,
                LastName = student.LastName,
                ContactPerson = student.ContactPerson,
                ContactNo = student.ContactNo,
                Email = student.Email,
                DateOfBirth = student.DateOfBirth.Date,
                Age = student.Age,
                ClassroomId = student.ClassroomId,
                Classroom = student.Classroom
            };
            return Ok(response);
        }

        //DELETE:https://localhost:7151/api/Categories/{id}
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] int id)
        {
            var student = await studentRepository.DeleteAsync(id);
            if (student is null)
            {
                return NotFound();
            }

            //convert Domain model to DTO
            var response = new StudentDto
            {
                StudentId = student.StudentId,
                FirstName = student.FirstName,
                LastName = student.LastName,
                ContactPerson = student.ContactPerson,
                ContactNo = student.ContactNo,
                Email = student.Email,
                DateOfBirth = student.DateOfBirth.Date,
                Age = student.Age,
                ClassroomId = student.ClassroomId,
                Classroom = student.Classroom
            };
            return Ok(response);
        }
    }
}
