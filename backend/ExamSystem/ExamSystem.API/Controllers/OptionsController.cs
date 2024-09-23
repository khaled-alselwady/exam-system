using ExamSystem.Entities;
using ExamSystem.Services.Options;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace ExamSystem.API.Controllers
{
    [RoutePrefix("api/options")]
    public class OptionsController : ApiController
    {
        private readonly OptionService _optionService;

        public OptionsController()
        {
            _optionService = new OptionService();
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetAllOptions()
        {
            try
            {
                var options = await _optionService.GetAllAsync();
                return options != null ? (IHttpActionResult)Ok(options) : NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception("An error occurred while retrieving options.", ex));
            }
        }

        [HttpGet]
        [Route("{id:int}", Name = "FindOptionById")]
        public async Task<IHttpActionResult> Find(int id)
        {
            try
            {
                var option = await _optionService.FindAsync(id);
                return option != null ? (IHttpActionResult)Ok(option) : NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception($"An error occurred while finding the option with ID {id}.", ex));
            }
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Add([FromBody] Option newOption)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var option = await _optionService.AddAsync(newOption);
                return option != null
                    ? (IHttpActionResult)CreatedAtRoute("FindOptionById", new { id = option.Id }, option)
                    : BadRequest();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception("An error occurred while adding a new option.", ex));
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Update(int id, [FromBody] Option updatedOption)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var option = await _optionService.UpdateAsync(id, updatedOption);
                return option != null ? (IHttpActionResult)Ok(option) : BadRequest();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception($"An error occurred while updating the option with ID {id}.", ex));
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            try
            {
                var isDeleted = await _optionService.RemoveAsync(id);
                return isDeleted ? (IHttpActionResult)StatusCode(HttpStatusCode.NoContent) : NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception($"An error occurred while deleting the option with ID {id}.", ex));
            }
        }

        [HttpGet]
        [Route("existsById")]
        public async Task<IHttpActionResult> ExistsById([FromUri] int id)
        {
            try
            {
                var isFound = await _optionService.ExistsByIdAsync(id);
                return isFound ? (IHttpActionResult)Ok(true) : NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception($"An error occurred while checking if the option with ID {id} exists.", ex));
            }
        }
    }
}
