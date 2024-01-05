using StudentManagementAPI.Models;

namespace StudentManagementAPI.Repositories.Interface
{
    public interface IClassroomRepository
    {
        Task<Classroom> CreateAsync(Classroom classroom);
        Task<IEnumerable<Classroom>> GetAllAsync();
        Task<Classroom?> GetById(int id);
        Task<Classroom?> UpdateAsync(Classroom classroom);
        Task<Classroom?> DeleteAsync(int id);
    }
}
