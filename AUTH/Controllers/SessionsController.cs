using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Swashbuckle.Swagger.Annotations;
using MeetUp.ApiTokenDemo.SDK;
using System.Threading;
using System.Threading.Tasks;

namespace MeetUp.ApiTokenDemo.AUTH.Controllers
{
    public class SessionsController : ApiController
    {
        // POST api/sessions
        // 已經註冊過的開發者，可以憑 APIKEY，向 AUTH 認證服務建立 SESSION。SESSION 建立成功後會取得 SESSION TOKEN，可以憑 TOKEN 道別的服務去呼叫 API。SESSION TOKEN 有效期限為 1hr
        [SwaggerOperation("Create")]
        [SwaggerResponse(HttpStatusCode.Created)]
        [SwaggerOperationFilter(typeof(AddApiKeyParameter))]
        //public void Post([FromBody]string value)
        public IHttpActionResult Post()
        {
            string apikey = this.Request.Headers.GetValues("X-APIKEY").First();

            ApiKeyToken apikeyToken = TokenHelper.DecodeToken<ApiKeyToken>("APIKEY", apikey);

            SessionToken sessionToken = TokenHelper.CreateToken<SessionToken>();
            sessionToken.ClientID = apikeyToken.ClientID;
            sessionToken.CreateDate = DateTime.Now;
            sessionToken.ExpireDate = DateTime.Now.AddHours(1.0);
            sessionToken.EnableAdminFunction = false;
            sessionToken.EnableMemberFunction = !apikeyToken.Tags.Contains("BAD");
            sessionToken.EnableVIPFunction = apikeyToken.Tags.Contains("VIP");

            return new TokenTextResult("SESSION", sessionToken);
            //return TokenHelper.EncodeToken("SESSION", sessionToken);
            //return this.Ok<string>(TokenHelper.EncodeToken("SESSION", sessionToken));
            //return this.Content<string>(HttpStatusCode.OK, TokenHelper.EncodeToken("SESSION", sessionToken));
        }



        /*
        // GET api/values
        [SwaggerOperation("GetAll")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [SwaggerOperation("GetById")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public string Get(int id)
        {
            return "value";
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
