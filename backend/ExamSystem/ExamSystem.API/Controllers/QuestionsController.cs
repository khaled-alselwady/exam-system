using ExamSystem.Entities;
using ExamSystem.Services.Questions;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace ExamSystem.API.Controllers
{
    [RoutePrefix("api/questions")]
    public class QuestionsController : ApiController
    {
        private readonly QuestionService _questionService;

        public QuestionsController()
        {
            _questionService = new QuestionService();
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetAllQuestions()
        {
            try
            {
                var questions = await _questionService.GetAllAsync();
                return questions != null ? (IHttpActionResult)Ok(questions) : NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception("An error occurred while retrieving questions.", ex));
            }
        }

        [HttpGet]
        [Route("{id:int}", Name = "FindQuestionById")]
        public async Task<IHttpActionResult> Find(int id)
        {
            try
            {
                var question = await _questionService.FindAsync(id);
                return question != null ? (IHttpActionResult)Ok(question) : NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception($"An error occurred while finding the question with ID {id}.", ex));
            }
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Add([FromBody] Question newQuestion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var question = await _questionService.AddAsync(newQuestion);
                return question != null
                    ? (IHttpActionResult)CreatedAtRoute("FindQuestionById", new { id = question.Id }, question)
                    : BadRequest();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception("An error occurred while adding a new question.", ex));
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Update(int id, [FromBody] Question updatedQuestion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var question = await _questionService.UpdateAsync(id, updatedQuestion);
                return question != null ? (IHttpActionResult)Ok(question) : BadRequest();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception($"An error occurred while updating the question with ID {id}.", ex));
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            try
            {
                var isDeleted = await _questionService.RemoveAsync(id);
                return isDeleted ? (IHttpActionResult)StatusCode(HttpStatusCode.NoContent) : NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception($"An error occurred while deleting the question with ID {id}.", ex));
            }
        }

        [HttpGet]
        [Route("existsById")]
        public async Task<IHttpActionResult> ExistsById([FromUri] int id)
        {
            try
            {
                var isFound = await _questionService.ExistsByIdAsync(id);
                return isFound ? (IHttpActionResult)Ok(true) : NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception($"An error occurred while checking if the question with ID {id} exists.", ex));
            }
        }

        [HttpGet]
        [Route("options")]
        public async Task<IHttpActionResult> GetAllOptions(int id)
        {
            try
            {
                var questions = await _questionService.GetAllOptionsAsync(id);
                return questions != null ? (IHttpActionResult)Ok(questions) : NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception($"An error occurred while retrieving options of the question with ID {id}.", ex));
            }
        }

        [HttpGet]
        [Route("right-option")]
        public async Task<IHttpActionResult> GetRightOption(int id)
        {
            try
            {
                var questions = await _questionService.GetRightOptionAsync(id);
                return questions != null ? (IHttpActionResult)Ok(questions) : NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception($"An error occurred while retrieving right option of the question with ID {id}.", ex));
            }
        }

        [HttpGet]
        [Route("check-right-answer")]
        public async Task<IHttpActionResult> IsRightAnswer([FromUri] int questionId, [FromUri] int selectedOptionId)
        {
            try
            {
                var isFound = await _questionService.IsRightOption(questionId, selectedOptionId);
                return isFound ? (IHttpActionResult)Ok(true) : NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception($"An error occurred while checking if the answer is correct or not.", ex));
            }
        }
    }
}
