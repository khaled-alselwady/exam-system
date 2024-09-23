using ExamSystem.Entities;
using ExamSystem.Services.StudentAnswers;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace ExamSystem.API.Controllers
{
    [RoutePrefix("api/student-answers")]
    public class StudentAnswersController : ApiController
    {
        private readonly StudentAnswerService _studentAnswerService;

        public StudentAnswersController()
        {
            _studentAnswerService = new StudentAnswerService();
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetAllStudentAnswers()
        {
            try
            {
                var studentAnswers = await _studentAnswerService.GetAllAsync();
                return studentAnswers != null ? (IHttpActionResult)Ok(studentAnswers) : NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception("An error occurred while retrieving answers.", ex));
            }
        }

        [HttpGet]
        [Route("{id:int}", Name = "FindAnswerById")]
        public async Task<IHttpActionResult> Find(int id)
        {
            try
            {
                var studentAnswer = await _studentAnswerService.FindAsync(id);
                return studentAnswer != null ? (IHttpActionResult)Ok(studentAnswer) : NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception($"An error occurred while finding the answer with ID {id}.", ex));
            }
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Add([FromBody] StudentAnswer newStudentAnswer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var answer = await _studentAnswerService.AddAsync(newStudentAnswer);
                return answer != null
                    ? (IHttpActionResult)CreatedAtRoute("FindAnswerById", new { id = answer.Id }, answer)
                    : BadRequest();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception("An error occurred while adding a new answer.", ex));
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Update(int id, [FromBody] StudentAnswer updatedStudentAnswer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var answer = await _studentAnswerService.UpdateAsync(id, updatedStudentAnswer);
                return answer != null ? (IHttpActionResult)Ok(answer) : BadRequest();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception($"An error occurred while updating the answer with ID {id}.", ex));
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            try
            {
                var isDeleted = await _studentAnswerService.RemoveAsync(id);
                return isDeleted ? (IHttpActionResult)StatusCode(HttpStatusCode.NoContent) : NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception($"An error occurred while deleting the answer with ID {id}.", ex));
            }
        }

        [HttpGet]
        [Route("existsById")]
        public async Task<IHttpActionResult> ExistsById([FromUri] int id)
        {
            try
            {
                var isFound = await _studentAnswerService.ExistsByIdAsync(id);
                return isFound ? (IHttpActionResult)Ok(true) : NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception($"An error occurred while checking if the answer with ID {id} exists.", ex));
            }
        }
    }
}
