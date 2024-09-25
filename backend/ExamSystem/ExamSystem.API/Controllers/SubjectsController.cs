using ExamSystem.Entities;
using ExamSystem.Services.Subjects;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace ExamSystem.API.Controllers
{
    [RoutePrefix("api/subjects")]
    public class SubjectsController : ApiController
    {
        private readonly SubjectService _subjectService;

        public SubjectsController()
        {
            _subjectService = new SubjectService();
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetAllSubjects()
        {
            try
            {
                var subjects = await _subjectService.GetAllAsync();
                return subjects != null ? (IHttpActionResult)Ok(subjects) : NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception("An error occurred while retrieving subjects.", ex));
            }
        }

        [HttpGet]
        [Route("{id:int}", Name = "FindSubjectById")]
        public async Task<IHttpActionResult> Find(byte id)
        {
            try
            {
                var subject = await _subjectService.FindAsync(id);
                return subject != null ? (IHttpActionResult)Ok(subject) : NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception($"An error occurred while finding the subject with ID {id}.", ex));
            }
        }

        [HttpGet]
        [Route("findByName", Name = "FindSubjectByName")]
        public async Task<IHttpActionResult> Find(string name)
        {
            try
            {
                var subject = await _subjectService.FindAsync(name);
                return subject != null ? (IHttpActionResult)Ok(subject) : NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception($"An error occurred while finding the subject with name {name}.", ex));
            }
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Add([FromBody] Subject newSubject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var subject = await _subjectService.AddAsync(newSubject);
                return subject != null
                    ? (IHttpActionResult)CreatedAtRoute("FindSubjectById", new { id = subject.Id }, subject)
                    : BadRequest();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception("An error occurred while adding a new subject.", ex));
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Update(byte id, [FromBody] Subject updatedSubject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var subject = await _subjectService.UpdateAsync(id, updatedSubject);
                return subject != null ? (IHttpActionResult)Ok(subject) : BadRequest();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception($"An error occurred while updating the subject with ID {id}.", ex));
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Delete(byte id)
        {
            try
            {
                var isDeleted = await _subjectService.RemoveAsync(id);
                return isDeleted ? (IHttpActionResult)StatusCode(HttpStatusCode.NoContent) : NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception($"An error occurred while deleting the subject with ID {id}.", ex));
            }
        }

        [HttpGet]
        [Route("existsById")]
        public async Task<IHttpActionResult> ExistsById([FromUri] byte id)
        {
            try
            {
                var isFound = await _subjectService.ExistsByIdAsync(id);
                return isFound ? (IHttpActionResult)Ok(true) : NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception($"An error occurred while checking if the subject with ID {id} exists.", ex));
            }
        }

        [HttpGet]
        [Route("existsByName")]
        public async Task<IHttpActionResult> ExistsByEmail([FromUri] string name)
        {
            try
            {
                var isFound = await _subjectService.ExistsByNameAsync(name);
                return isFound ? (IHttpActionResult)Ok(true) : NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception("An error occurred while checking if the subject name exists.", ex));
            }
        }

        [HttpGet]
        [Route("questions")]
        public async Task<IHttpActionResult> GetAllQuestions(byte id)
        {
            try
            {
                var questions = await _subjectService.GetAllQuestionAsync(id);
                return questions != null ? (IHttpActionResult)Ok(questions) : NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception("An error occurred while retrieving questions.", ex));
            }
        }

        [HttpGet]
        [Route("count-questions/{id}")]
        public async Task<IHttpActionResult> GetQuestionsCountForSpecificSubject(byte id)
        {
            try
            {
                var questions = await _subjectService.GetQuestionsCountForSpecificSubjectAsync(id);
                return Ok(questions);
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception("An error occurred while retrieving questions count.", ex));
            }
        }
    }
}
