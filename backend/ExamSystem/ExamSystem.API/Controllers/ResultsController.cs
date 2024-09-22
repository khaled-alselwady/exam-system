using ExamSystem.Entities;
using ExamSystem.Services.Results;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace ExamSystem.API.Controllers
{
    [RoutePrefix("api/results")]
    public class ResultsController : ApiController
    {
        private readonly ResultService _resultService;

        public ResultsController()
        {
            _resultService = new ResultService();
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetAllResults()
        {
            try
            {
                var results = await _resultService.GetAllAsync();
                return results != null ? (IHttpActionResult)Ok(results) : NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception("An error occurred while retrieving results.", ex));
            }
        }

        [HttpGet]
        [Route("{id:int}", Name = "FindResultById")]
        public async Task<IHttpActionResult> Find(int id)
        {
            try
            {
                var result = await _resultService.FindAsync(id);
                return result != null ? (IHttpActionResult)Ok(result) : NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception($"An error occurred while finding the result with ID {id}.", ex));
            }
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Add([FromBody] Result newResult)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _resultService.AddAsync(newResult);
                return result != null
                    ? (IHttpActionResult)CreatedAtRoute("FindResultById", new { id = result.Id }, result)
                    : BadRequest();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception("An error occurred while adding a new result.", ex));
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Update(int id, [FromBody] Result updatedResult)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _resultService.UpdateAsync(id, updatedResult);
                return result != null ? (IHttpActionResult)Ok(result) : BadRequest();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception($"An error occurred while updating the result with ID {id}.", ex));
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            try
            {
                var isDeleted = await _resultService.RemoveAsync(id);
                return isDeleted ? (IHttpActionResult)StatusCode(HttpStatusCode.NoContent) : NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception($"An error occurred while deleting the result with ID {id}.", ex));
            }
        }

        [HttpGet]
        [Route("existsById")]
        public async Task<IHttpActionResult> ExistsById([FromUri] int id)
        {
            try
            {
                var isFound = await _resultService.ExistsByIdAsync(id);
                return isFound ? (IHttpActionResult)Ok(true) : NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception($"An error occurred while checking if the result with ID {id} exists.", ex));
            }
        }
    }
}
