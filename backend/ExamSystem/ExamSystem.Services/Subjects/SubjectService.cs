using ExamSystem.DataAccess.Data;
using ExamSystem.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace ExamSystem.Services.Subjects
{
    public class SubjectService
    {
        private readonly AppDbContext _context;
        private readonly SubjectValidation _subjectValidation;

        public SubjectService()
        {
            _context = new AppDbContext();
            _subjectValidation = new SubjectValidation();
        }

        public async Task<List<Subject>> GetAllAsync()
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

        public async Task<Subject> FindAsync(byte id)
        {
            try
            {
                return await _context.Subjects
                    .AsNoTracking()
                    .FirstOrDefaultAsync(s => s.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while finding the subject with ID {id}.", ex);
            }
        }

        public async Task<Subject> FindAsync(string name)
        {
            try
            {
                return await _context.Subjects
                    .AsNoTracking()
                    .FirstOrDefaultAsync(s => s.Name.ToLower() == name.ToLower());
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while finding the subject with name {name}.", ex);
            }
        }

        public async Task<Subject> AddAsync(Subject newSubject)
        {
            try
            {
                if (!await _subjectValidation.IsValid(newSubject, this))
                {
                    return null;
                }

                //var subject = new Subject { Name = newSubject.Name };
                _context.Subjects.Add(newSubject);
                await _context.SaveChangesAsync();

                return newSubject;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding a new subject.", ex);
            }
        }

        public async Task<Subject> UpdateAsync(byte id, Subject updatedSubject)
        {
            try
            {
                if (!await _subjectValidation.IsValid(updatedSubject, this))
                {
                    return null;
                }

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

        public async Task<bool> RemoveAsync(byte id)
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

        public async Task<bool> ExistsByIdAsync(byte id)
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

        public async Task<bool> ExistsByNameAsync(string name)
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

        public async Task<bool> NotExistsByNameAsync(string name)
        {
            try
            {
                return !await ExistsByNameAsync(name);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while checking if the subject name does not exist.", ex);
            }
        }

        public async Task<List<Question>> GetAllQuestionAsync(byte id)
        {
            var questions = await _context.Questions
                                .AsNoTracking()
                                .Include(q => q.Options)
                                .Where(q => q.SubjectId == id)
                                .ToListAsync();

            return questions;
        }

        public async Task<short> GetQuestionsCountForSpecificSubjectAsync(byte id)
        {
            var countQuestions = await _context.Questions
                                    .AsNoTracking()
                                    .CountAsync(q => q.SubjectId == id);

            return (short)countQuestions;
        }
    }
}
