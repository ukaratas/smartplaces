﻿using System;
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

        [HttpDelete("Region/{regionId}")]
        [ProducesErrorResponseType(typeof(void))]
        [ProducesResponseType(typeof(SaveResponse<Region>), 200)]
        public SaveResponse<Region> DeleteRegion(Guid regionId)
        {
            return _settingsService.DeleteRegion(regionId);

        }


        [HttpDelete("Section/{sectionId}")]
        [ProducesErrorResponseType(typeof(void))]
        [ProducesResponseType(typeof(SaveResponse<Section>), 200)]
        public SaveResponse<Section> DeleteSection(Guid sectionId)
        {
            return _settingsService.DeleteSection(sectionId);

        }

        [HttpDelete("GadgetDefinition/{gadgetDefinitionId}")]
        [ProducesErrorResponseType(typeof(void))]
        [ProducesResponseType(typeof(SaveResponse<GadgetDefinition>), 200)]
        public SaveResponse<GadgetDefinition> DeleteGadgetDefinition(Guid gadgetDefinitionId)
        {
            return _settingsService.DeleteGadgetDefinition(gadgetDefinitionId);

        }

        [HttpDelete("Gadget/{gadgetId}")]
        [ProducesErrorResponseType(typeof(void))]
        [ProducesResponseType(typeof(SaveResponse<Gadget>), 200)]
        public SaveResponse<Gadget> DeleteGadget(Guid gadgetId)
        {
            return _settingsService.DeleteGadget(gadgetId);

        }
        
    }
}
