using ExamSystem.DataAccess.Data;
using ExamSystem.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace ExamSystem.Services
{
    public class StudentService
    {
        private readonly AppDbContext _context;

        public StudentService()
        {
            _context = new AppDbContext();
        }

        public async Task<List<Student>> GetAll()
        {
            return await _context.Students
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Student> Find(int id)
        {
            return await _context.Students
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Student> Add(Student newStudent)
        {
            _context.Students.Add(newStudent);
            await _context.SaveChangesAsync();

            return newStudent;
        }

        public async Task<Student> Update(int id, Student updatedStudent)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return null;
            }

            _context.Entry(student).CurrentValues.SetValues(updatedStudent);
            await _context.SaveChangesAsync();

            return student;
        }

        public async Task<bool> Remove(int id)
        {
            var studentToRemove = await _context.Students.FindAsync(id);
            if (studentToRemove == null)
            {
                return false;
            }

            var result = _context.Students.Remove(studentToRemove);
            await _context.SaveChangesAsync();

            return result != null;
        }

        public async Task<bool> Exists(int id)
        {
            return await _context.Students.AsNoTracking().AnyAsync(s => s.Id == id);
        }
    }
}
