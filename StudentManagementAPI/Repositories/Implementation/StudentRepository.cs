using Microsoft.EntityFrameworkCore;
using StudentManagementAPI.Data;
using StudentManagementAPI.Models;
using StudentManagementAPI.Repositories.Interface;

namespace StudentManagementAPI.Repositories.Implementation
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext dbContext;

        public StudentRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Student> CreateAsync(Student student)
        {
            await dbContext.Students.AddAsync(student);
            await dbContext.SaveChangesAsync();

            return student;
        }

        public async Task<Student?> DeleteAsync(int studentId)
        {
            var existingStudent = await dbContext.Students.FirstOrDefaultAsync(x => x.StudentId == studentId);

            if (existingStudent is null)
            {
                return null;
            }

            dbContext.Students.Remove(existingStudent);
            await dbContext.SaveChangesAsync();
            return existingStudent;
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await dbContext.Students.ToListAsync();
        }

        public async Task<Student?> GetById(int id)
        {
            return await dbContext.Students.FirstOrDefaultAsync(x => x.StudentId == id);
        }

        

        public async Task<Student?> UpdateAsync(Student student)
        {
            var existingStudent = await dbContext.Students.FirstOrDefaultAsync(x => x.StudentId == student.StudentId);

            if (existingStudent != null)
            {
                dbContext.Entry(existingStudent).CurrentValues.SetValues(student);
                await dbContext.SaveChangesAsync();
                return student;
            }

            return null;
        }
    }
}
