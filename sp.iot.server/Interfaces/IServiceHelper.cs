using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;


namespace sp.iot.server
{
    public interface IServiceHelper
    {
        ObjectResult CreateResponse(int code, object message);
        
    }

}