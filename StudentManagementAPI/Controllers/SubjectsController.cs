using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagementAPI.Models.DTO;
using StudentManagementAPI.Models;
using StudentManagementAPI.Repositories.Interface;

namespace StudentManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly ISubjectRepository subjectRepository;

        public SubjectsController(ISubjectRepository subjectRepository)
        {
            this.subjectRepository = subjectRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubject([FromBody] CreateSubjectRequestDto request)
        {


            //Map DTO to Domain Model


            var subject = new Subject
            {
                SubjectName= request.SubjectName,

            };

            await subjectRepository.CreateAsync(subject);

            //Map Domain model to DTO

            var response = new SubjectDto
            {
                SubjectId = subject.SubjectId,
                SubjectName= subject.SubjectName,
            };

            return Ok(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllSubjects()
        {
            var subjects = await subjectRepository.GetAllAsync();

            //Map Domain model to DTO

            var response = new List<SubjectDto>();

            foreach (var subject in subjects)
            {
                response.Add(new SubjectDto
                {

                    SubjectId = subject.SubjectId,
                    SubjectName= subject.SubjectName,
                });
            }

            return Ok(response);
        }
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetSubjectById([FromRoute] int id)
        {
            var existingSubject = await subjectRepository.GetById(id);

            //Map Domain model to DTO
            if (existingSubject is null)
            {
                return NotFound();
            }
            var response = new SubjectDto
            {

                SubjectId = existingSubject.SubjectId,
                SubjectName= existingSubject.SubjectName,
            };



            return Ok(response);
        }

        //PUT:https://localhost:7151/api/Categories/{id}
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> EditSubject([FromRoute] int id, UpdateSubjectRequestDto request)
        {
            //category DTO to Domain Model
            var subject = new Subject
            {
                SubjectId = id,
               SubjectName= request.SubjectName,

            };

            subject = await subjectRepository.UpdateAsync(subject);

            if (subject == null)
            {
                return NotFound();
            }
            //Convert Domain model to DTO
            var response = new SubjectDto
            {
                SubjectId = subject.SubjectId,
                SubjectName = subject.SubjectName,

            };
            return Ok(response);
        }

        //DELETE:https://localhost:7151/api/Categories/{id}
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] int id)
        {
            var subject = await subjectRepository.DeleteAsync(id);
            if (subject is null)
            {
                return NotFound();
            }

            //convert Domain model to DTO
            var response = new SubjectDto
            {
                SubjectId = subject.SubjectId,
               SubjectName= subject.SubjectName,
            };
            return Ok(response);
        }
    }
}
