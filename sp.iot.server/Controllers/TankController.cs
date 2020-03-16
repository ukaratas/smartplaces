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
    public class TankController : ControllerBase
    {

        private readonly ILogger<TankController> _logger;
        private readonly ITankService _tankService;

        public TankController(ILogger<TankController> logger, ITankService tankService)
        {
            _logger = logger;
            _tankService = tankService;
        }

        /// <summary>
        /// Return requested tank information.
        /// </summary>
        /// <param name="id">Unique Tank id as Guid format</param>
        /// <returns>Tank information</returns>
        /// <response code="452">If the item is not exists</response>  
        [HttpGet("{id}")]
        [ProducesErrorResponseType(typeof(void))]
        [ProducesResponseType(typeof(Tank), 200)]
        [ProducesResponseType(typeof(string), 452)]
        public ActionResult<Tank> GetTank([FromRoute(Name = "id")] Guid? id)
        {
            var returnValue = _tankService.Get(id.Value);

            if (returnValue != null)
            {
                return Ok(returnValue);
            }
            else
            {
                return StatusCode(452, "No tank record found");
            }
        }

        /// <summary>
        /// Return tank list.
        /// </summary>
        /// <param name="type">Tank type.If submitted returning set filtered with that.</param>
        /// <returns>Tank list</returns>
        [HttpGet()]
        [ProducesErrorResponseType(typeof(void))]
        [ProducesResponseType(typeof(Tank[]), 200)]
        public ActionResult<IEnumerable<Tank>> GetTanks([FromQuery(Name = "type")] TankType? type)
        {
            return Ok(_tankService.Get(type));
        }

        /// <summary>
        /// Saves tank information in request body.null If Tank(id) is exists updates it, if not creates it.
        /// </summary>
        /// <param name="body">Tank information that going to save.</param>
        /// <returns>Tank information</returns>
        /// <response code="452">Error occured while saving item.</response>  
        [HttpPost()]
        [ProducesErrorResponseType(typeof(void))]
        [ProducesResponseType(typeof(Tank), 200)]
        [ProducesResponseType(typeof(Dictionary<DateTime, string>), 452)]
        public ActionResult<SaveResponse<Tank>> Save([FromBody] TankSaveRequest body)
        {
            SaveResponse<Tank> returnValue = _tankService.Save(body);

            if (returnValue.Status != SaveResponseType.Error)
                return Ok(returnValue);
            else
                return StatusCode(452, returnValue.Actions);
        }
    }
}
