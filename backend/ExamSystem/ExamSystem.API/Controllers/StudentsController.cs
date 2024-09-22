using ExamSystem.Entities;
using ExamSystem.Services;
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
            var students = await _studentService.GetAll();

            if (students == null)
            {
                return NotFound();
            }

            return Ok(students);
        }

        [HttpGet]
        [Route("{id:int}", Name = "FindStudentById")]
        public async Task<IHttpActionResult> Find(int id)
        {
            var student = await _studentService.Find(id);

            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Add([FromBody] Student newStudent)
        {
            if (newStudent == null)
            {
                return BadRequest();
            }

            var student = await _studentService.Add(newStudent);

            if (student == null)
            {
                return BadRequest();
            }

            return CreatedAtRoute("FindStudentById", new { id = student.Id }, student);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Update(int id, [FromBody] Student updatedStudent)
        {
            if (updatedStudent == null)
            {
                return BadRequest();
            }

            var student = await _studentService.Update(id, updatedStudent);

            if (student == null)
            {
                return BadRequest();
            }

            return CreatedAtRoute("FindStudentById", new { id = student.Id }, student);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var isDeleted = await _studentService.Remove(id);

            if (isDeleted)
            {
                return Ok(true);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("exists/{id:int}")]
        public async Task<IHttpActionResult> ExistsById(int id)
        {
            var isFound = await _studentService.Exists(id);

            if (isFound)
            {
                return Ok(true);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
