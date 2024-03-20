using Goodlord_TechnicalAssessment_AdamHassall.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Goodlord_TechnicalAssessment_AdamHassall.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AffordablePropertiesController : ControllerBase
    {

        private readonly AffordabilityCheckService _affordabilityCheckService;

        public AffordablePropertiesController(AffordabilityCheckService affordabilityCheckService)
        {
            _affordabilityCheckService = affordabilityCheckService;
        }

        // GET: api/<AffordablePropertiesController>
        [HttpGet]
        public IEnumerable<Property> Get()
        {
            return _affordabilityCheckService.GetListOfAffordableProperties();
        }

       
    }
}
