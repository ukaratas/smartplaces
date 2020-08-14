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
    
        try
        {
            settings = await client.GetFromJsonAsync<Settings>("Settings");
        }
        catch
        {
            
        }
    
        return settings;
    }
}