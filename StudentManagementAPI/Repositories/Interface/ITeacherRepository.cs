using StudentManagementAPI.Models;

namespace StudentManagementAPI.Repositories.Interface
{
    public interface ITeacherRepository
    {
        Task<Teacher> CreateAsync(Teacher teacher);
        Task<IEnumerable<Teacher>> GetAllAsync();
        Task<Teacher?> GetById(int id);
        Task<Teacher?> UpdateAsync(Teacher teacher);
        Task<Teacher?> DeleteAsync(int id);
    }
}
