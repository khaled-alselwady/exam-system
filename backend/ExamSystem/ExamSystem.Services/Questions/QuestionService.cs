using ExamSystem.DataAccess.Data;
using ExamSystem.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace ExamSystem.Services.Questions
{
    public class QuestionService
    {
        private readonly AppDbContext _context;

        public QuestionService()
        {
            _context = new AppDbContext();
        }

        private bool _IsQuestionValid(Question question)
        {
            return question != null && !string.IsNullOrWhiteSpace(question.Text);
        }

        public async Task<List<Question>> GetAllAsync()
        {
            try
            {
                return await _context.Questions.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving questions.", ex);
            }
        }

        public async Task<Question> FindAsync(int id)
        {
            try
            {
                return await _context.Questions.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while finding the question with ID {id}.", ex);
            }
        }

        public async Task<Question> AddAsync(Question newQuestion)
        {
            try
            {
                if (!_IsQuestionValid(newQuestion))
                {
                    return null;
                }

                _context.Questions.Add(newQuestion);
                await _context.SaveChangesAsync();

                return newQuestion;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding a new question.", ex);
            }
        }

        public async Task<Question> UpdateAsync(int id, Question updatedQuestion)
        {
            try
            {
                if (!_IsQuestionValid(updatedQuestion))
                {
                    return null;
                }

                var question = await _context.Questions.FindAsync(id);

                if (question == null)
                {
                    return null;
                }

                _context.Entry(question).CurrentValues.SetValues(updatedQuestion);
                await _context.SaveChangesAsync();

                return question;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while updating the question with ID {id}.", ex);
            }
        }

        public async Task<bool> RemoveAsync(int id)
        {
            try
            {
                var result = await _context.Database.ExecuteSqlCommandAsync("DELETE FROM Questions WHERE Id = @p0", id);
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while removing the question with ID {id}.", ex);
            }
        }

        public async Task<bool> ExistsByIdAsync(int id)
        {
            try
            {
                return await _context.Questions.AsNoTracking().AnyAsync(s => s.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while checking if the question with ID {id} exists.", ex);
            }
        }

        public async Task<List<Option>> GetAllOptionsAsync(int questionId)
        {
            var options = await _context.Options
                            .Where(o => o.QuestionId == questionId)
                            .ToListAsync();

            return options;
        }

        public async Task<Option> GetRightOptionAsync(int questionId)
        {
            var rightOption = await _context.Options
                                .Where(o => o.QuestionId == questionId && o.IsCorrect)
                                .SingleOrDefaultAsync();

            return rightOption;
        }

        public async Task<bool> IsRightOption(int questionId, int selectedOptionId)
        {
            bool isRightOption = await _context.Options
                                    .AnyAsync(o =>
                                    o.QuestionId == questionId &&
                                    o.Id == selectedOptionId &&
                                    o.IsCorrect);

            return isRightOption;
        }
    }
}
