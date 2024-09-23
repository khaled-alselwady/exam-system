using ExamSystem.DataAccess.Data;
using ExamSystem.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace ExamSystem.Services.StudentAnswers
{
    public class StudentAnswerService
    {
        private readonly AppDbContext _context;

        public StudentAnswerService()
        {
            _context = new AppDbContext();
        }

        public async Task<List<StudentAnswer>> GetAllAsync()
        {
            try
            {
                return await _context.StudentAnswers.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving answers.", ex);
            }
        }

        public async Task<StudentAnswer> FindAsync(int id)
        {
            try
            {
                return await _context.StudentAnswers.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while finding the answer with ID {id}.", ex);
            }
        }

        public async Task<StudentAnswer> AddAsync(StudentAnswer newStudentAnswer)
        {
            try
            {
                _context.StudentAnswers.Add(newStudentAnswer);
                await _context.SaveChangesAsync();

                return newStudentAnswer;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding a new answer.", ex);
            }
        }

        public async Task<StudentAnswer> UpdateAsync(int id, StudentAnswer updatedStudentAnswer)
        {
            try
            {
                var studentAnswer = await _context.StudentAnswers.FindAsync(id);

                if (studentAnswer == null)
                {
                    return null;
                }

                _context.Entry(studentAnswer).CurrentValues.SetValues(updatedStudentAnswer);
                await _context.SaveChangesAsync();

                return studentAnswer;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while updating the answer with ID {id}.", ex);
            }
        }

        public async Task<bool> RemoveAsync(int id)
        {
            try
            {
                var result = await _context.Database.ExecuteSqlCommandAsync("DELETE FROM StudentAnswers WHERE Id = @p0", id);
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while removing the answer with ID {id}.", ex);
            }
        }

        public async Task<bool> ExistsByIdAsync(int id)
        {
            try
            {
                return await _context.StudentAnswers.AsNoTracking().AnyAsync(s => s.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while checking if the answer with ID {id} exists.", ex);
            }
        }
    }
}
