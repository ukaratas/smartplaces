using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;


namespace sp.iot.server
{
    public class ServiceHelper : IServiceHelper
    {
        public ObjectResult CreateResponse(int code, object message)
        {

            
            //return StatusCode(code, message);

            return null;
        }


    }

}