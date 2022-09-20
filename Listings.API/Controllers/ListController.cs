using Microsoft.AspNetCore.Mvc;
using Listings.Models;
using Listings.API.Services;

namespace Listings.API.Controllers
{
    public class ListController : CrudControllerBase<ListRecord, int>
    {
        public ListController(ILogger<ListController> logger, IGenericCrudService<ListRecord, int> crudService) : base(logger, crudService)
        {
            
        }

        [HttpPatch("{id}")]
        public virtual async Task<IActionResult> Patch(int id, [FromQuery] string name)
        {

            ListRecord? recordToUpdate;

            try
            {
                recordToUpdate = await _genericCrudService.GetByIdAsync(id);
            }
            catch (Exception)
            {
                throw;
            }

            if (recordToUpdate == null)
                return NotFound();

            recordToUpdate.Name = name;

            return Ok(recordToUpdate);
        }

    }
}