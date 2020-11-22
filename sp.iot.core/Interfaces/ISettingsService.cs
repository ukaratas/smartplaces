using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;


namespace sp.iot.core
{
    public interface ISettingsService
    {
        Settings Get();
        SaveResponse<Settings> Save(Settings request);

        SaveResponse<Section> DeleteSection(Guid sectionId);
        SaveResponse<Region> DeleteRegion(Guid regionId);
    }
}