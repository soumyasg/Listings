using Listings.API.Services;
using Listings.Data;
using Listings.Models;
using Microsoft.AspNetCore.Mvc;

namespace Listings.API.Controllers
{
    //[ApiController]
    //[Route("[controller]")]
    public class ListController : CrudControllerBase<ListRecord, int>
    {
        public ListController(ILogger<ListController> logger, IGenericCrudService<ListRecord, int> crudService) : base(logger, crudService) { }
        

    }
}