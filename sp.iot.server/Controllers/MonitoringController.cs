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
    public class MonitoringController : ControllerBase
    {

        private readonly ILogger<MonitoringController> _logger;
        private readonly ITankService _tankService;

        public MonitoringController(ILogger<MonitoringController> logger, ITankService tankService)
        {
            _logger = logger;
            _tankService = tankService;
        }


        [HttpGet("Liquid")]
        public IEnumerable<TankMonitoring> LiquidGetMonitoring([FromQuery(Name = "offset")] int offset, [FromQuery(Name = "limit")] int limit)
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

        [HttpPost("Liquid/New")]
        [ProducesErrorResponseType(typeof(void))]
        [ProducesResponseType(typeof(TankMonitoring), 200)]
        public IEnumerable<Tank> LiquidCreateMonitoring([FromBody] Guid tankId)
        {
            throw new NotImplementedException();
        }



        [HttpPost("Liquid/Stop")]
        [ProducesErrorResponseType(typeof(void))]
        [ProducesResponseType(typeof(TankMonitoring), 200)]
        public IEnumerable<Tank> LiquidStopMonitoring([FromBody] Guid tankMonitoringId)
        {
            throw new NotImplementedException();
        }

        [HttpGet("Electric")]
        public IEnumerable<TankMonitoring> ElectricetMonitoring([FromQuery(Name = "offset")] int offset, [FromQuery(Name = "limit")] int limit)
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

        [HttpPost("Electric/New")]
        [ProducesErrorResponseType(typeof(void))]
        [ProducesResponseType(typeof(TankMonitoring), 200)]
        public IEnumerable<Tank> ElectricCreateMonitoring([FromBody] Guid tankId)
        {
            throw new NotImplementedException();
        }



        [HttpPost("Electric/Stop")]
        [ProducesErrorResponseType(typeof(void))]
        [ProducesResponseType(typeof(TankMonitoring), 200)]
        public IEnumerable<Tank> ElectricStopMonitoring([FromBody] Guid tankMonitoringId)
        {
            throw new NotImplementedException();
        }
    }
}
