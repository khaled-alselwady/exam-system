using ExamSystem.Entities;
using ExamSystem.Services;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace ExamSystem.API.Controllers
{
    [RoutePrefix("api/students")]
    public class StudentsController : ApiController
    {
        private readonly StudentService _studentService;

        public StudentsController()
        {
            _studentService = new StudentService();
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetAllStudents()
        {
            try
            {
                var students = await _studentService.GetAll();
                return students != null ? (IHttpActionResult)Ok(students) : NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception("An error occurred while retrieving students.", ex));
            }
        }

        [HttpGet]
        [Route("{id:int}", Name = "FindStudentById")]
        public async Task<IHttpActionResult> Find(int id)
        {
            try
            {
                var student = await _studentService.Find(id);
                return student != null ? (IHttpActionResult)Ok(student) : NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception($"An error occurred while finding the student with ID {id}.", ex));
            }
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Add([FromBody] Student newStudent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var student = await _studentService.Add(newStudent);
                return student != null
                    ? (IHttpActionResult)CreatedAtRoute("FindStudentById", new { id = student.Id }, student)
                    : BadRequest();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception("An error occurred while adding a new student.", ex));
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Update(int id, [FromBody] Student updatedStudent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var student = await _studentService.Update(id, updatedStudent);
                return student != null ? (IHttpActionResult)Ok(student) : BadRequest();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception($"An error occurred while updating the student with ID {id}.", ex));
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            try
            {
                var isDeleted = await _studentService.Remove(id);
                return isDeleted ? (IHttpActionResult)StatusCode(HttpStatusCode.NoContent) : NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception($"An error occurred while deleting the student with ID {id}.", ex));
            }
        }

        [HttpGet]
        [Route("existsById")]
        public async Task<IHttpActionResult> ExistsById([FromUri] int id)
        {
            try
            {
                var isFound = await _studentService.ExistsById(id);
                return isFound ? (IHttpActionResult)Ok(true) : NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception($"An error occurred while checking if the student with ID {id} exists.", ex));
            }
        }

        [HttpGet]
        [Route("existsByEmail")]
        public async Task<IHttpActionResult> ExistsByEmail([FromUri] string email)
        {
            try
            {
                var isFound = await _studentService.ExistsByEmail(email);
                return isFound ? (IHttpActionResult)Ok(true) : NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception("An error occurred while checking if the email exists.", ex));
            }
        }
    }
}
