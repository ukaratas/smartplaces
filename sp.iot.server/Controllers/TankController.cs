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

        [HttpGet()]
        [ProducesErrorResponseType(typeof(void))]
        [ProducesResponseType(typeof(Tank[]), 200)]
        public IEnumerable<Tank> GetTanks([FromQuery(Name = "type")] TankType? type)
        {
            return _tankService.GetTanks(type);
        }

        [HttpGet("Monitoring")]
        public IEnumerable<TankMonitoring> GetMonitoring()
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
    }
}
