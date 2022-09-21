using Listings.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Listings.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CrudControllerBase<TModel, TKey> : ControllerBase where TModel : class where TKey : struct
    {

        protected readonly ILogger<CrudControllerBase<TModel, TKey>> _logger;
        protected readonly IGenericCrudService<TModel, TKey> _genericCrudService;

        public CrudControllerBase(ILogger<CrudControllerBase<TModel, TKey>> logger,
            IGenericCrudService<TModel, TKey> genericCrudService)
        {
            _logger = logger;
            _genericCrudService = genericCrudService;
        }

        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<TModel>>> Get()
        {

            var records = await _genericCrudService.GetAllAsync();

            return Ok(records);
        }



        [HttpGet("{id}")]
        public virtual async Task<ActionResult<TModel>> Get(TKey id)
        {
            var record = await _genericCrudService.GetByIdAsync(id);

            if (record == null)
            {
                return NotFound();
            }

            return record;
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(TKey id)
        {
            await _genericCrudService.DeleteAsync(id);

            return NoContent();
        }

        [HttpPost]
        public virtual async Task<ActionResult<TModel>> Post(TModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newModel = await _genericCrudService.AddAsync(model);

            return CreatedAtAction(nameof(Get), new { id = GetId(newModel) }, newModel);
        }

        [HttpPut("{id}")]
        public virtual async Task<IActionResult> Put(TKey id, TModel model)
        {
            if (!id.Equals(GetId(model)))
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            TModel? updatedEntity;

            try
            {
                updatedEntity = await _genericCrudService.UpdateAsync(model);
            }
            catch (Exception)
            {
                throw;
            }

            return Ok(updatedEntity);
        }

        protected virtual async Task<bool> RecordExists(TKey id)
        {
            return (await _genericCrudService.GetByIdAsync(id)) != null;
        }

        // This is a hacky way to get the Id. In a real life app, we would make TModel implement an interface that
        // will guarantee that TModel will always have an Id property.
        private TKey GetId(TModel model)
        {
            var idProp = model.GetType().GetProperty("Id");
            var idValue = idProp?.GetValue(model, null);
            return idValue != null ? (TKey) idValue : default;
        }
    }
}
