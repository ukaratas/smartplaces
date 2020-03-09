using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace sp.iot.server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TankController : ControllerBase
    {

        private readonly ILogger<TankController> _logger;

        public TankController(ILogger<TankController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Tank> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new Tank
            {
                Id = Guid.NewGuid(),
                StartDate = DateTime.Now.AddDays(-index),
                FinishDate = DateTime.Now,
                Consumption = 55.5,
            })
            .ToArray();
        }
    }
}
