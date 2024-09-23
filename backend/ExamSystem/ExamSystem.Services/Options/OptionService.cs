using ExamSystem.DataAccess.Data;
using ExamSystem.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace ExamSystem.Services.Options
{
    public class OptionService
    {
        private readonly AppDbContext _context;

        public OptionService()
        {
            _context = new AppDbContext();
        }

        private bool _IsOptionValid(Option option)
        {
            return option != null && !string.IsNullOrWhiteSpace(option.Text);
        }

        public async Task<List<Option>> GetAllAsync()
        {
            try
            {
                return await _context.Options.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving options.", ex);
            }
        }

        public async Task<Option> FindAsync(int id)
        {
            try
            {
                return await _context.Options.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while finding the option with ID {id}.", ex);
            }
        }

        public async Task<Option> AddAsync(Option newOption)
        {
            try
            {
                if (!_IsOptionValid(newOption))
                {
                    return null;
                }

                _context.Options.Add(newOption);
                await _context.SaveChangesAsync();

                return newOption;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding a new option.", ex);
            }
        }

        public async Task<Option> UpdateAsync(int id, Option updatedOption)
        {
            try
            {
                if (!_IsOptionValid(updatedOption))
                {
                    return null;
                }

                var Option = await _context.Options.FindAsync(id);

                if (Option == null)
                {
                    return null;
                }

                _context.Entry(Option).CurrentValues.SetValues(updatedOption);
                await _context.SaveChangesAsync();

                return Option;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while updating the option with ID {id}.", ex);
            }
        }

        public async Task<bool> RemoveAsync(int id)
        {
            try
            {
                var result = await _context.Database.ExecuteSqlCommandAsync("DELETE FROM Options WHERE Id = @p0", id);
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while removing the option with ID {id}.", ex);
            }
        }

        public async Task<bool> ExistsByIdAsync(int id)
        {
            try
            {
                return await _context.Options.AsNoTracking().AnyAsync(s => s.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while checking if the option with ID {id} exists.", ex);
            }
        }
    }
}
