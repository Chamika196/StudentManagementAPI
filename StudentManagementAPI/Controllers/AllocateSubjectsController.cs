using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagementAPI.Models.DTO;
using StudentManagementAPI.Models;
using StudentManagementAPI.Repositories.Implementation;
using StudentManagementAPI.Repositories.Interface;

namespace StudentManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AllocateSubjectsController : ControllerBase
    {
        private readonly IAllocateSubjectRepository allocateSubjectRepository;
        public AllocateSubjectsController(IAllocateSubjectRepository allocateSubjectRepository)
        {
            this.allocateSubjectRepository = allocateSubjectRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAllocateSubject([FromBody] CreateAllocateSubjectRequestDto request)
        {
            // Log or debug the SubjectId to check its value
            Console.WriteLine($"SubjectId receivedaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa: {request.SubjectId}");

            var allocateSubject = new AllocateSubject
            {
                SubjectId = request.SubjectId,
                TeacherId = request.TeacherId,
            };

            await allocateSubjectRepository.CreateAsync(allocateSubject);

            var response = new AllocateSubjectDto
            {
                AllocateSubjectId = allocateSubject.AllocateSubjectId,
                SubjectId = allocateSubject.SubjectId,
                TeacherId = allocateSubject.TeacherId,
            };

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAllocateSubjects()
        {
            var allocateSubjects = await allocateSubjectRepository.GetAllAsync();

            //Map Domain model to DTO

            var response = new List<AllocateSubjectDto>();

            foreach (var allocateSubject in allocateSubjects)
            {
                response.Add(new AllocateSubjectDto
                {

                    AllocateSubjectId = allocateSubject.AllocateSubjectId,
                    SubjectId = allocateSubject.SubjectId,
                    TeacherId = allocateSubject.TeacherId,
                });
            }

            return Ok(response);
        }
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetAllocateSubjectById([FromRoute] int id)
        {
            var existingAllocateSubject = await allocateSubjectRepository.GetById(id);

            //Map Domain model to DTO
            if (existingAllocateSubject is null)
            {
                return NotFound();
            }
            var response = new AllocateSubjectDto
            {

                AllocateSubjectId = existingAllocateSubject.AllocateSubjectId,
                SubjectId= existingAllocateSubject.SubjectId,
                TeacherId= existingAllocateSubject.TeacherId,
            };



            return Ok(response);
        }

        //PUT:https://localhost:7151/api/Categories/{id}
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> EditAllocateSubject([FromRoute] int id, UpdateAllocateSubjectRequestDto request)
        {
            //category DTO to Domain Model
            var allocateSubject = new AllocateSubject
            {
                AllocateSubjectId = id,
                SubjectId= request.SubjectId,
                TeacherId= request.TeacherId,

            };

            allocateSubject = await allocateSubjectRepository.UpdateAsync(allocateSubject);

            if (allocateSubject == null)
            {
                return NotFound();
            }
            //Convert Domain model to DTO
            var response = new AllocateSubjectDto
            {
                AllocateSubjectId = allocateSubject.AllocateSubjectId,
                SubjectId= allocateSubject.SubjectId,
                TeacherId= allocateSubject.TeacherId,
            };
            return Ok(response);
        }

        //DELETE:https://localhost:7151/api/Categories/{id}
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] int id)
        {
            var allocateSubject = await allocateSubjectRepository.DeleteAsync(id);
            if (allocateSubject is null)
            {
                return NotFound();
            }

            //convert Domain model to DTO
            var response = new AllocateSubjectDto
            {
                AllocateSubjectId = allocateSubject.AllocateSubjectId,
                SubjectId = allocateSubject.SubjectId,
                TeacherId = allocateSubject.TeacherId,
            };
            return Ok(response);
        }
    }
}
