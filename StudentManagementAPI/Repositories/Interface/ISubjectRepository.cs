using StudentManagementAPI.Models;

namespace StudentManagementAPI.Repositories.Interface
{
    public interface ISubjectRepository
    {
        Task<Subject> CreateAsync(Subject subject);
        Task<IEnumerable<Subject>> GetAllAsync();
        Task<Subject?> GetById(int id);
        Task<Subject?> UpdateAsync(Subject subject);
        Task<Subject?> DeleteAsync(int id);
    }
}
