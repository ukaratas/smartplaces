using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using sp.iot.core;

namespace sp.iot.server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GadgetController : ControllerBase
    {

        private readonly ILogger<GadgetController> _logger;
        private readonly IGadgetService _gadgetService;

        public GadgetController(ILogger<GadgetController> logger, IGadgetService gadgetService)
        {
            _logger = logger;
            _gadgetService = gadgetService;
        }

        /// <summary>
        /// Return requested gadget information.
        /// </summary>
        /// <remarks>8de4c111-f63a-46a2-8f4c-152ef1271410</remarks>
        /// <param name="id" example="8de4c111-f63a-46a2-8f4c-152ef1271410">Unique gadget id as Guid format</param>
        /// <returns>Gadget  information</returns>
        /// <response code="452">Gadget is not exists</response>  
        [HttpGet("GetById/{id}")]
        [ProducesErrorResponseType(typeof(void))]
        [ProducesResponseType(typeof(Gadget), 200)]
        public Gadget GetById(Guid id)
        {
            return _gadgetService.Get(id);
        }

        [HttpGet("GetFiltered")]
        [ProducesErrorResponseType(typeof(void))]
        [ProducesResponseType(typeof(IEnumerable<Gadget>), 200)]
        public IEnumerable<Gadget> GetFiltered(Guid region, Guid section, [FromQuery(Name = "type-group")] GadgetTypeGroup? typeGroup, GadgetType? type)
        {
            return _gadgetService.GetFiltered(region, section, typeGroup, type);
        }

        [HttpPost("SetValue/{id}")]
        [ProducesErrorResponseType(typeof(void))]
        [ProducesResponseType(typeof(SaveResponse<string>), 200)]
        public SaveResponse<string> SetValue(Guid id, [FromBody] GadgetSetValueRequest value)
        {
            return _gadgetService.SetValue(id, value);
        }
    }
}
