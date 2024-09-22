using ExamSystem.Entities;
using ExamSystem.Services.Exams;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace ExamSystem.API.Controllers
{
    [RoutePrefix("api/exams")]
    public class ExamsController : ApiController
    {
        private readonly ExamService _examService;
        public ExamsController()
        {
            _examService = new ExamService();
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetAllExams()
        {
            try
            {
                var exams = await _examService.GetAllAsync();
                return exams != null ? (IHttpActionResult)Ok(exams) : NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception("An error occurred while retrieving exams.", ex));
            }
        }

        [HttpGet]
        [Route("{id:int}", Name = "FindExamById")]
        public async Task<IHttpActionResult> Find(int id)
        {
            try
            {
                var exam = await _examService.FindAsync(id);
                return exam != null ? (IHttpActionResult)Ok(exam) : NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception($"An error occurred while finding the exam with ID {id}.", ex));
            }
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Add([FromBody] Exam newExam)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var exam = await _examService.AddAsync(newExam);
                return exam != null
                    ? (IHttpActionResult)CreatedAtRoute("FindExamById", new { id = exam.Id }, exam)
                    : BadRequest();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception("An error occurred while adding a new exam.", ex));
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Update(int id, [FromBody] Exam updatedExam)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var exam = await _examService.UpdateAsync(id, updatedExam);
                return exam != null ? (IHttpActionResult)Ok(exam) : BadRequest();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception($"An error occurred while updating the exam with ID {id}.", ex));
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            try
            {
                var isDeleted = await _examService.RemoveAsync(id);
                return isDeleted ? (IHttpActionResult)StatusCode(HttpStatusCode.NoContent) : NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception($"An error occurred while deleting the exam with ID {id}.", ex));
            }
        }

        [HttpGet]
        [Route("existsById")]
        public async Task<IHttpActionResult> ExistsById([FromUri] int id)
        {
            try
            {
                var isFound = await _examService.ExistsByIdAsync(id);
                return isFound ? (IHttpActionResult)Ok(true) : NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception($"An error occurred while checking if the exam with ID {id} exists.", ex));
            }
        }
    }
}
