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
    public class ElectricController : ControllerBase
    {

        private readonly ILogger<ElectricController> _logger;
        private readonly ITankService _tankService;

        public ElectricController(ILogger<ElectricController> logger, ITankService tankService)
        {
            _logger = logger;
            _tankService = tankService;
        }

        [HttpGet("Switch/{id}")]
        [ProducesErrorResponseType(typeof(void))]
        [ProducesResponseType(typeof(Tank), 200)]
        [ProducesResponseType(typeof(void), 204)]
        public Tank GetSwitch([FromRoute(Name = "id")] Guid? id)
        {
           throw new NotImplementedException();
        }

        [HttpGet("Switch")]
        [ProducesErrorResponseType(typeof(void))]
        [ProducesResponseType(typeof(Tank[]), 200)]
        public IEnumerable<Tank> GetSwitches([FromQuery(Name = "type")] TankType? type)
        {
            throw new NotImplementedException();
        }


        [HttpPost("Switch")]
        [ProducesErrorResponseType(typeof(void))]
        [ProducesResponseType(typeof(Tank), 200)]
        public IEnumerable<Tank> SaveSwitch([FromBody] TankSaveRequest body)
        {
            throw new NotImplementedException();
        }

        [HttpGet("Socket/{id}")]
        [ProducesErrorResponseType(typeof(void))]
        [ProducesResponseType(typeof(Tank), 200)]
        [ProducesResponseType(typeof(void), 204)]
        public Tank GetSocket([FromRoute(Name = "id")] Guid? id)
        {
            throw new NotImplementedException();
        }

        [HttpGet("Sockets")]
        [ProducesErrorResponseType(typeof(void))]
        [ProducesResponseType(typeof(Tank[]), 200)]
        public IEnumerable<Tank> GetSockets([FromQuery(Name = "type")] TankType? type)
        {
            throw new NotImplementedException();
        }


        [HttpPost("Sockets")]
        [ProducesErrorResponseType(typeof(void))]
        [ProducesResponseType(typeof(Tank), 200)]
        public IEnumerable<Tank> SaveTank([FromBody] TankSaveRequest body)
        {
            throw new NotImplementedException();
        }
    }
}
