using ExamSystem.DataAccess.Data;
using ExamSystem.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace ExamSystem.Services.Subjects
{
    public class SubjectService
    {
        private readonly AppDbContext _context;

        public SubjectService()
        {
            _context = new AppDbContext();
        }

        public async Task<List<Subject>> GetAll()
        {
            try
            {
                return await _context.Subjects.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving subjects.", ex);
            }
        }

        public async Task<Subject> Find(int id)
        {
            try
            {
                return await _context.Subjects.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while finding the subject with ID {id}.", ex);
            }
        }

        public async Task<Subject> Add(Subject newSubject)
        {
            try
            {
                //if (!await _subjectValidation.IsValid(newSubject, this))
                //{
                //    return null;
                //}

                _context.Subjects.Add(newSubject);
                await _context.SaveChangesAsync();

                return newSubject;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding a new subject.", ex);
            }
        }

        public async Task<Subject> Update(int id, Subject updatedSubject)
        {
            try
            {
                //if (!await _studentValidation.IsValid(updatedStudent, this))
                //{
                //    return null;
                //}

                if (updatedSubject == null || id != updatedSubject.Id)
                {
                    return null;
                }

                var subject = await _context.Subjects.FindAsync(id);

                if (subject == null)
                {
                    return null;
                }

                _context.Entry(subject).CurrentValues.SetValues(updatedSubject);
                await _context.SaveChangesAsync();

                return subject;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while updating the subject with ID {id}.", ex);
            }
        }

        public async Task<bool> Remove(int id)
        {
            try
            {
                var result = await _context.Database.ExecuteSqlCommandAsync("DELETE FROM Subjects WHERE Id = @p0", id);
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while removing the subject with ID {id}.", ex);
            }
        }

        public async Task<bool> ExistsById(int id)
        {
            try
            {
                return await _context.Subjects.AsNoTracking().AnyAsync(s => s.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while checking if the subject with ID {id} exists.", ex);
            }
        }

        public async Task<bool> ExistsByName(string name)
        {
            try
            {
                return await _context.Subjects.AsNoTracking().AnyAsync(s => s.Name.ToLower() == name.ToLower());
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while checking if the subject name exists.", ex);
            }
        }

        public async Task<bool> NotExistsByName(string name)
        {
            try
            {
                return !await ExistsByName(name);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while checking if the subject name does not exist.", ex);
            }
        }
    }
}
