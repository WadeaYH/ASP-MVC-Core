using FirstMVCCore.Models;

namespace FirstMVCCore.Service
{
    public interface IStudentService
    {
        List<Student> GetAll();
        Student? GetById(int id);                // ? means it can return null
        void Add(Student student);
        void Update(Student student);
        void Delete(int id);
    }
}
