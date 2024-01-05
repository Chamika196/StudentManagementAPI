using Microsoft.EntityFrameworkCore;
using StudentManagementAPI.Data;
using StudentManagementAPI.Models;
using StudentManagementAPI.Repositories.Interface;

namespace StudentManagementAPI.Repositories.Implementation
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly ApplicationDbContext dbContext;
        public TeacherRepository(ApplicationDbContext dbContext )
        {
               this.dbContext = dbContext;
        }
        public async Task<Teacher> CreateAsync(Teacher teacher)
        {
            await dbContext.Teachers.AddAsync(teacher);
            await dbContext.SaveChangesAsync();

            return teacher;
        }

        public async Task<Teacher?> DeleteAsync(int teacherId)
        {
            var existingTeacher = await dbContext.Teachers.FirstOrDefaultAsync(x => x.TeacherId == teacherId);

            if (existingTeacher is null)
            {
                return null;
            }

            dbContext.Teachers.Remove(existingTeacher);
            await dbContext.SaveChangesAsync();
            return existingTeacher;
        }

        public async Task<IEnumerable<Teacher>> GetAllAsync()
        {
            return await dbContext.Teachers.ToListAsync();
        }

        public async Task<Teacher?> GetById(int teacherId)
        {
            return await dbContext.Teachers.FirstOrDefaultAsync(x => x.TeacherId == teacherId);
        }

        public async Task<Teacher?> UpdateAsync(Teacher teacher)
        {
            var existingTeacher = await dbContext.Teachers.FirstOrDefaultAsync(x => x.TeacherId == teacher.TeacherId);

            if (existingTeacher != null)
            {
                dbContext.Entry(existingTeacher).CurrentValues.SetValues(teacher);
                await dbContext.SaveChangesAsync();
                return teacher;
            }

            return null;
        }
    }
}
