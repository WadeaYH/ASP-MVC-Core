using FirstMVCCore.Models;
using System.Xml.Linq;

namespace FirstMVCCore.Service
{
    public class StudentService : IStudentService     // "implements" is ":" in C#
    {
        // fake database (static = shared between requests)
        private static List<Student> _students = new()
    {
        new Student { Id = 1, Name = "Ali" },
        new Student { Id = 2, Name = "Sara" }
    };

        public List<Student> GetAll() => _students;

        public Student? GetById(int id) =>
            _students.FirstOrDefault(s => s.Id == id);   // like stream().filter().findFirst()

        public void Add(Student student)
        {
            student.Id = _students.Count == 0 ? 1 : _students.Max(s => s.Id) + 1;  // fake auto-increment
            _students.Add(student);
        }

        public void Update(Student student)
        {
            var old = GetById(student.Id);
            if (old != null) old.Name = student.Name;
        }

        public void Delete(int id)
        {
            var s = GetById(id);
            if (s != null) _students.Remove(s);
        }
    }
}
