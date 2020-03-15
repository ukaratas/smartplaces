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
    public class TanksController : ControllerBase
    {

        private readonly ILogger<TanksController> _logger;
        private readonly ITankService _tankService;

        public TanksController(ILogger<TanksController> logger, ITankService tankService)
        {
            _logger = logger;
            _tankService = tankService;
        }

        /// <summary>
        /// Return requested tank information.
        /// </summary>
        /// <param name="id">Unique Tank id as Guid format</param>
        /// <returns>Tank information</returns>
        /// <response code="204">If the item is not exists</response>  
        [HttpGet("Tank/{id}")]
        [ProducesErrorResponseType(typeof(void))]
        [ProducesResponseType(typeof(Tank), 200)]
        [ProducesResponseType(typeof(void), 204)]
        public Tank GetTank([FromRoute(Name = "id")] Guid? id)
        {
            var returnValue = _tankService.GetTank(id.Value);

            if (returnValue != null)
            {
                return returnValue;
            }
            else
            {
                StatusCode(204);
                return null;
            }
        }

        [HttpGet("Tank")]
        [ProducesErrorResponseType(typeof(void))]
        [ProducesResponseType(typeof(Tank[]), 200)]
        public IEnumerable<Tank> GetTanks([FromQuery(Name = "type")] TankType? type)
        {
            return _tankService.GetTanks(type);
        }


        [HttpPost("Tank")]
        [ProducesErrorResponseType(typeof(void))]
        [ProducesResponseType(typeof(Tank), 200)]
        public IEnumerable<Tank> SaveTank([FromBody] TankSaveRequest body)
        {
            throw new NotImplementedException();
        }

        [HttpGet("Monitoring")]
        public IEnumerable<TankMonitoring> GetMonitoring([FromQuery(Name = "offset")] int offset, [FromQuery(Name = "limit")] int limit)
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new TankMonitoring
            {
                Id = Guid.NewGuid(),
                StartDate = DateTime.Now.AddDays(-index),
                FinishDate = DateTime.Now.AddDays(+index),
                Consumption = index * 11.5,
            })
            .ToArray();
        }

        [HttpPost("Monitoring/New")]
        [ProducesErrorResponseType(typeof(void))]
        [ProducesResponseType(typeof(TankMonitoring), 200)]
        public IEnumerable<Tank> CreateMonitoring([FromBody] Guid tankId)
        {
            throw new NotImplementedException();
        }



        [HttpPost("Monitoring/Stop")]
        [ProducesErrorResponseType(typeof(void))]
        [ProducesResponseType(typeof(TankMonitoring), 200)]
        public IEnumerable<Tank> StopMonitoring([FromBody] Guid tankMonitoringId)
        {
            throw new NotImplementedException();
        }
    }
}
