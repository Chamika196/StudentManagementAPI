using StudentManagementAPI.Models;

namespace StudentManagementAPI.Repositories.Interface
{
    public interface IStudentRepository
    {
        Task<Student> CreateAsync(Student student);
        Task<IEnumerable<Student>> GetAllAsync();
        Task<Student?> GetById(int id);
        Task<Student?> UpdateAsync(Student student);
        Task<Student?> DeleteAsync(int id);
    }
}
