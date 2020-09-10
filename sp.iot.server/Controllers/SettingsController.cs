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
    public class SettingsController : ControllerBase
    {

        private readonly ILogger<SettingsController> _logger;
        private readonly ISettingsService _settingsService;

        public SettingsController(ILogger<SettingsController> logger, ISettingsService settingsService)
        {
            _logger = logger;
            _settingsService = settingsService;
        }

        [HttpGet()]
        [ProducesErrorResponseType(typeof(void))]
        [ProducesResponseType(typeof(Settings), 200)]
        public Settings GetSettings()
        {
            var returnValue = _settingsService.Get();
            return returnValue;
        }

        [HttpPost()]
        [ProducesErrorResponseType(typeof(void))]
        [ProducesResponseType(typeof(SaveResponse<Settings>), 200)]
        public SaveResponse<Settings> SaveSwitch([FromBody] Settings body)
        {
            return _settingsService.Save(body);

        }

        [HttpDelete("Section/{sectionId}")]
        [ProducesErrorResponseType(typeof(void))]
        [ProducesResponseType(typeof(SaveResponse<Section>), 200)]
        public SaveResponse<Section> DeleteSection(Guid sectionId)
        {
            return _settingsService.DeleteSection(sectionId);

        }
    }
}
