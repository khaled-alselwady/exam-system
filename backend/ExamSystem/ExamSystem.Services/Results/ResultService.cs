using ExamSystem.DataAccess.Data;
using ExamSystem.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace ExamSystem.Services.Results
{
    public class ResultService
    {
        private readonly AppDbContext _context;

        public ResultService()
        {
            _context = new AppDbContext();
        }

        public async Task<List<Result>> GetAllAsync()
        {
            try
            {
                return await _context.Results.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving results.", ex);
            }
        }

        public async Task<Result> FindAsync(int id)
        {
            try
            {
                return await _context.Results.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while finding the result with ID {id}.", ex);
            }
        }

        public async Task<Result> AddAsync(Result newResult)
        {
            try
            {
                //if (!await _examValidation.IsValid(newExam, this))
                //{
                //    return null;
                //}

                _context.Results.Add(newResult);
                await _context.SaveChangesAsync();

                return newResult;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding a new result.", ex);
            }
        }

        public async Task<Result> UpdateAsync(int id, Result updatedResult)
        {
            try
            {
                //if (!await _examValidation.IsValid(updatedExam, this))
                //{
                //    return null;
                //}

                if (updatedResult == null || id != updatedResult.Id)
                {
                    return null;
                }

                var result = await _context.Results.FindAsync(id);

                if (result == null)
                {
                    return null;
                }

                _context.Entry(result).CurrentValues.SetValues(updatedResult);
                await _context.SaveChangesAsync();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while updating the result with ID {id}.", ex);
            }
        }

        public async Task<bool> RemoveAsync(int id)
        {
            try
            {
                var result = await _context.Database.ExecuteSqlCommandAsync("DELETE FROM Results WHERE Id = @p0", id);
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while removing the result with ID {id}.", ex);
            }
        }

        public async Task<bool> ExistsByIdAsync(int id)
        {
            try
            {
                return await _context.Results.AsNoTracking().AnyAsync(s => s.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while checking if the result with ID {id} exists.", ex);
            }
        }
    }
}
