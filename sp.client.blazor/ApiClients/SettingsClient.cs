using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using sp.iot.core;

public class SettingsClient
{
    private readonly HttpClient client;

    public SettingsClient(HttpClient client)
    {
        this.client = client;

    }

    public async Task<Settings> GetAsync()
    {
        var settings = new Settings();

        /*ry
        {*/
        settings = await client.GetFromJsonAsync<Settings>("Settings");
        /*}
        catch (Exception ex)
        {
            throw ex;
            //return null;
        }*/

        return settings;
    }


    public async Task<SaveResponse<Settings>> SaveAsync(Settings settings)
    {

        SaveResponse<Settings> result = null;
        try
        {
            using (var response = await client.PostAsJsonAsync("Settings", settings))
            {

                result = await response.Content.ReadAsAsync<SaveResponse<Settings>>();

            }
        }
        catch
        {
            throw;
        }

        return result;
    }

    public async Task<SaveResponse<Section>> DeleteSectionAsync(Guid sectionId)
    {

        SaveResponse<Section> result = null;
        try
        {
            using (var response = await client.DeleteAsync("Settings/section/" + sectionId.ToString()))
            {

                result = await response.Content.ReadAsAsync<SaveResponse<Section>>();
            }
        }
        catch
        {
            throw;
        }

        return result;
    }

    public async Task<SaveResponse<Region>> DeleteRegionAsync(Guid regionId)
    {

        SaveResponse<Region> result = null;
        try
        {
            using (var response = await client.DeleteAsync("Settings/region/" + regionId.ToString()))
            {

                result = await response.Content.ReadAsAsync<SaveResponse<Region>>();
            }
        }
        catch
        {
            throw;
        }

        return result;
    }



    public async Task<SaveResponse<Gadget>> DeleteGadgetAsync(Guid gadgetId)
    {

        SaveResponse<Gadget> result = null;
       

        return result;
    }



    public async Task<SaveResponse<GadgetDefinition>> DeleteGadgetDefinitionAsync(Guid gadgetDefinitionId)
    {

        SaveResponse<GadgetDefinition> result = null;
        try
        {
            using (var response = await client.DeleteAsync("Settings/gadgetdefinition/" + gadgetDefinitionId.ToString()))
            {

                result = await response.Content.ReadAsAsync<SaveResponse<GadgetDefinition>>();
            }
        }
        catch
        {
            throw;
        }

        return result;
    }
}