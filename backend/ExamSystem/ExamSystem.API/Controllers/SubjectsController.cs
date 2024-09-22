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
        public async Task<IHttpActionResult> Find(int id)
        {
            try
            {
                var student = await _subjectService.FindAsync(id);
                return student != null ? (IHttpActionResult)Ok(student) : NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception($"An error occurred while finding the subject with ID {id}.", ex));
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
        public async Task<IHttpActionResult> Update(int id, [FromBody] Subject updatedSubject)
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
        public async Task<IHttpActionResult> Delete(int id)
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
        public async Task<IHttpActionResult> ExistsById([FromUri] int id)
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
    }
}
