using Microsoft.EntityFrameworkCore;
using StudentManagementAPI.Data;
using StudentManagementAPI.Models;
using StudentManagementAPI.Repositories.Interface;

namespace StudentManagementAPI.Repositories.Implementation
{
    public class AllocateSubjectRepository : IAllocateSubjectRepository

    {
        private readonly ApplicationDbContext dbContext;
        public AllocateSubjectRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<AllocateSubject> CreateAsync(AllocateSubject allocateSubject)
        {
            // Check if the subject with the specified SubjectId exists
            var subjectExists = await dbContext.Subjects.AnyAsync(s => s.SubjectId == allocateSubject.SubjectId);

            if (!subjectExists)
            {
                // Handle the case where the subject does not exist
                // You may throw an exception, log an error, or handle it based on your application's requirements.
                throw new Exception($"Subject with SubjectId '{allocateSubject.SubjectId}' does not exist.");
            }

            // Continue with the insertion logic
            await dbContext.AllocateSubjects.AddAsync(allocateSubject);
            await dbContext.SaveChangesAsync();

            return allocateSubject;
        }


        public async Task<AllocateSubject?> DeleteAsync(int allocateSubjectId)
        {
            var existingAllocateSubject = await dbContext.AllocateSubjects.FirstOrDefaultAsync(x => x.AllocateSubjectId == allocateSubjectId);

            if (existingAllocateSubject is null)
            {
                return null;
            }

            dbContext.AllocateSubjects.Remove(existingAllocateSubject);
            await dbContext.SaveChangesAsync();
            return existingAllocateSubject;
        }

        public async Task<IEnumerable<AllocateSubject>> GetAllAsync()
        {
            return await dbContext.AllocateSubjects.ToListAsync();
        }

        public async Task<AllocateSubject?> GetById(int allocateSubjectId)
        {
            return await dbContext.AllocateSubjects.FirstOrDefaultAsync(x => x.AllocateSubjectId == allocateSubjectId);
        }

        public async Task<AllocateSubject?> UpdateAsync(AllocateSubject allocateSubject)
        {
            var existingAllocateSubject = await dbContext.AllocateSubjects.FirstOrDefaultAsync(x => x.AllocateSubjectId == allocateSubject.AllocateSubjectId);

            if (existingAllocateSubject != null)
            {
                dbContext.Entry(existingAllocateSubject).CurrentValues.SetValues(allocateSubject);
                await dbContext.SaveChangesAsync();
                return allocateSubject;
            }

            return null;
        }
    }
}
