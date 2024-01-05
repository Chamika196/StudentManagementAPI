using Microsoft.EntityFrameworkCore;
using StudentManagementAPI.Data;
using StudentManagementAPI.Models;
using StudentManagementAPI.Repositories.Interface;

namespace StudentManagementAPI.Repositories.Implementation
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SubjectRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Subject> CreateAsync(Subject subject)
        {
            await dbContext.Subjects.AddAsync(subject);
            await dbContext.SaveChangesAsync();

            return subject;
        }

        public async Task<Subject?> DeleteAsync(int subjectId)
        {
            var existingSubject = await dbContext.Subjects.FirstOrDefaultAsync(x => x.SubjectId == subjectId);

            if (existingSubject is null)
            {
                return null;
            }

            dbContext.Subjects.Remove(existingSubject);
            await dbContext.SaveChangesAsync();
            return existingSubject;
        }

        public async Task<IEnumerable<Subject>> GetAllAsync()
        {
            return await dbContext.Subjects.ToListAsync();
        }

        public async Task<Subject?> GetById(int subjectId)
        {
            return await dbContext.Subjects.FirstOrDefaultAsync(x => x.SubjectId == subjectId);
        }

        public async Task<Subject?> UpdateAsync(Subject subject)
        {
            var existingSubject = await dbContext.Subjects.FirstOrDefaultAsync(x => x.SubjectId == subject.SubjectId);

            if (existingSubject != null)
            {
                dbContext.Entry(existingSubject).CurrentValues.SetValues(subject);
                await dbContext.SaveChangesAsync();
                return subject;
            }

            return null;
        }
    }
}
