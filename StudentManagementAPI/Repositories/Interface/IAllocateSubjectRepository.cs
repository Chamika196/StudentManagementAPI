using StudentManagementAPI.Models;

namespace StudentManagementAPI.Repositories.Interface
{
    public interface IAllocateSubjectRepository
    {
        Task<AllocateSubject> CreateAsync(AllocateSubject allocateSubject);
        Task<IEnumerable<AllocateSubject>> GetAllAsync();
        Task<AllocateSubject?> GetById(int id);
        Task<AllocateSubject?> UpdateAsync(AllocateSubject allocateSubject);
        Task<AllocateSubject?> DeleteAsync(int id);
    }
}
