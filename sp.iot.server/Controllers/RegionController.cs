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
    public class RegionController : ControllerBase
    {

        private readonly ILogger<RegionController> _logger;
        private readonly ITankService _tankService;

        public RegionController(ILogger<RegionController> logger, ITankService tankService)
        {
            _logger = logger;
            _tankService = tankService;
        }

        [HttpGet("{id}")]
        [ProducesErrorResponseType(typeof(void))]
        [ProducesResponseType(typeof(Tank), 200)]
        [ProducesResponseType(typeof(void), 204)]
        public Tank GetRegion([FromRoute(Name = "id")] Guid? id)
        {
           throw new NotImplementedException();
        }

        [HttpGet()]
        [ProducesErrorResponseType(typeof(void))]
        [ProducesResponseType(typeof(Tank[]), 200)]
        public IEnumerable<Tank> GetTanks([FromQuery(Name = "type")] TankType? type)
        {
           throw new NotImplementedException();
        }


        [HttpPost()]
        [ProducesErrorResponseType(typeof(void))]
        [ProducesResponseType(typeof(Tank), 200)]
        public IEnumerable<Tank> SaveTank([FromBody] TankSaveRequest body)
        {
            throw new NotImplementedException();
        }
    }
}
