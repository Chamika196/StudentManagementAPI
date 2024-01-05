using Microsoft.EntityFrameworkCore;
using StudentManagementAPI.Data;
using StudentManagementAPI.Models;
using StudentManagementAPI.Repositories.Interface;

namespace StudentManagementAPI.Repositories.Implementation
{
    public class ClassroomRepository : IClassroomRepository
    {
        private readonly ApplicationDbContext dbContext;

        public ClassroomRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Classroom> CreateAsync(Classroom classroom)
        {
            await dbContext.Classrooms.AddAsync(classroom);
            await dbContext.SaveChangesAsync();

            return classroom;
        }

        public async Task<Classroom?> DeleteAsync(int classroomsId)
        {
            var existingClassroom= await dbContext.Classrooms.FirstOrDefaultAsync(x => x.ClassroomId == classroomsId);

            if (existingClassroom is null)
            {
                return null;
            }

            dbContext.Classrooms.Remove(existingClassroom);
            await dbContext.SaveChangesAsync();
            return existingClassroom;
        }

        public async Task<IEnumerable<Classroom>> GetAllAsync()
        {
            return await dbContext.Classrooms.ToListAsync();
        }

        public async Task<Classroom?> GetById(int classroomsId)
        {
            return await dbContext.Classrooms.FirstOrDefaultAsync(x => x.ClassroomId == classroomsId);
        }

        public async Task<Classroom?> UpdateAsync(Classroom classroom)
        {
            var existingClassroom = await dbContext.Classrooms.FirstOrDefaultAsync(x => x.ClassroomId == classroom.ClassroomId);

            if (existingClassroom != null)
            {
                dbContext.Entry(existingClassroom).CurrentValues.SetValues(classroom);
                await dbContext.SaveChangesAsync();
                return classroom;
            }

            return null;
        }
    }
}
