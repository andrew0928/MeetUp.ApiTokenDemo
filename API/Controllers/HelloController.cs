using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Swashbuckle.Swagger.Annotations;
using MeetUp.ApiTokenDemo.SDK;

namespace MeetUp.ApiTokenDemo.API.Controllers
{
    public class HelloController : ApiController
    {
        // GET api/values
        [SwaggerOperation("GetAll")]
        public object Get()
        {
            string tokentext = this.Request.Headers.GetValues("X-SESSION").First();
            SessionToken session = TokenHelper.DecodeToken<SessionToken>("SESSION", tokentext);

            return new
            {
                Message = $"Hello, {session.ClientID}!",
                IsVIP = session.EnableVIPFunction
            };
        }


        /*
        // GET api/values/5
        [SwaggerOperation("GetById")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [SwaggerOperation("Create")]
        [SwaggerResponse(HttpStatusCode.Created)]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [SwaggerOperation("Update")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [SwaggerOperation("Delete")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public void Delete(int id)
        {
        }
        */
    }
}
