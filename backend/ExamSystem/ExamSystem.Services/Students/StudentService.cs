using ExamSystem.DataAccess.Data;
using ExamSystem.Entities;
using ExamSystem.Services.Students;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace ExamSystem.Services
{
    public class StudentService
    {
        private readonly AppDbContext _context;
        private readonly StudentValidation _studentValidation;

        public StudentService()
        {
            _context = new AppDbContext();
            _studentValidation = new StudentValidation();
        }

        public async Task<List<Student>> GetAll()
        {
            try
            {
                return await _context.Students.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving students.", ex);
            }
        }

        public async Task<Student> Find(int id)
        {
            try
            {
                return await _context.Students.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while finding the student with ID {id}.", ex);
            }
        }

        public async Task<Student> Add(Student newStudent)
        {
            try
            {
                if (!await _studentValidation.IsValid(newStudent, this))
                {
                    return null;
                }

                _context.Students.Add(newStudent);
                await _context.SaveChangesAsync();

                return newStudent;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding a new student.", ex);
            }
        }

        public async Task<Student> Update(int id, Student updatedStudent)
        {
            try
            {
                if (!await _studentValidation.IsValid(updatedStudent, this))
                {
                    return null;
                }

                if (updatedStudent == null || id != updatedStudent.Id)
                {
                    return null;
                }

                var student = await _context.Students.FindAsync(id);

                if (student == null)
                {
                    return null;
                }

                _context.Entry(student).CurrentValues.SetValues(updatedStudent);
                await _context.SaveChangesAsync();

                return student;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while updating the student with ID {id}.", ex);
            }
        }

        public async Task<bool> Remove(int id)
        {
            try
            {
                //var studentToRemove = await _context.Students.FindAsync(id);
                //if (studentToRemove == null)
                //{
                //    return false;
                //}

                //var result = _context.Students.Remove(studentToRemove);
                //await _context.SaveChangesAsync();

                var result = await _context.Database.ExecuteSqlCommandAsync("DELETE FROM Students WHERE Id = @p0", id);
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while removing the student with ID {id}.", ex);
            }
        }

        public async Task<bool> ExistsById(int id)
        {
            try
            {
                return await _context.Students.AsNoTracking().AnyAsync(s => s.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while checking if the student with ID {id} exists.", ex);
            }
        }

        public async Task<bool> ExistsByEmail(string email)
        {
            try
            {
                return await _context.Students.AsNoTracking().AnyAsync(s => s.Email.ToLower() == email.ToLower());
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while checking if the email exists.", ex);
            }
        }

        public async Task<bool> NotExistsByEmail(string email)
        {
            try
            {
                return !await ExistsByEmail(email);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while checking if the email does not exist.", ex);
            }
        }
    }
}
