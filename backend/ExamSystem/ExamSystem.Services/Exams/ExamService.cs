using ExamSystem.DataAccess.Data;
using ExamSystem.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace ExamSystem.Services.Exams
{
    public class ExamService
    {
        private readonly AppDbContext _context;
        public ExamService()
        {
            _context = new AppDbContext();
        }

        public async Task<List<Exam>> GetAllAsync()
        {
            try
            {
                return await _context.Exams.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving exams.", ex);
            }
        }

        public async Task<Exam> FindAsync(int id)
        {
            try
            {
                return await _context.Exams.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while finding the exam with ID {id}.", ex);
            }
        }

        public async Task<Exam> AddAsync(Exam newExam)
        {
            try
            {
                _context.Exams.Add(newExam);
                await _context.SaveChangesAsync();

                return newExam;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding a new exam.", ex);
            }
        }

        public async Task<Exam> UpdateAsync(int id, Exam updatedExam)
        {
            try
            {
                if (updatedExam == null || id != updatedExam.Id)
                {
                    return null;
                }

                var exam = await _context.Exams.FindAsync(id);

                if (exam == null)
                {
                    return null;
                }

                _context.Entry(exam).CurrentValues.SetValues(updatedExam);
                await _context.SaveChangesAsync();

                return exam;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while updating the exam with ID {id}.", ex);
            }
        }

        public async Task<bool> RemoveAsync(int id)
        {
            try
            {
                var result = await _context.Database.ExecuteSqlCommandAsync("DELETE FROM Exams WHERE Id = @p0", id);
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while removing the exam with ID {id}.", ex);
            }
        }

        public async Task<bool> ExistsByIdAsync(int id)
        {
            try
            {
                return await _context.Exams.AsNoTracking().AnyAsync(s => s.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while checking if the exam with ID {id} exists.", ex);
            }
        }
    }
}
