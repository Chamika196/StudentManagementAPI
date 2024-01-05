namespace StudentManagementAPI.Models.DTO
{
    public class UpdateStudentRequestDto
    {
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactPerson { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int? Age { get; set; }
        public int ClassroomId { get; set; }
        public Classroom Classroom { get; set; }
    }
}
