using MeetUp.ApiTokenDemo.SDK;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security;
using System.Web.Http;

namespace MeetUp.ApiTokenDemo.API.Controllers
{
    public class DiceController : ApiController
    {
        private Random _rnd = new Random();


        // GET api/<controller>/5

        /// <summary>
        /// 骰出指定次數的骰子
        /// </summary>
        /// <param name="count">要骰幾次?</param>
        /// <returns></returns>
        [SwaggerOperation("GetMany")]
        [SwaggerOperationFilter(typeof(AddSessionTokenParameter))]
        public IEnumerable<int> Get(int count)
        {
            // session check
            SessionToken session = TokenHelper.DecodeToken<SessionToken>(
                "SESSION", 
                this.Request.Headers.GetValues("X-SESSION").First());

            if (session.UserHostAddress != System.Web.HttpContext.Current.Request.UserHostAddress)
            {
                //throw new SecurityException("IP address not valid.");
                //return this.Request.CreateErrorResponse(HttpStatusCode.Forbidden, "This method is not allowed!");
                throw new HttpResponseException(new HttpResponseMessage() {
                    StatusCode = HttpStatusCode.Forbidden,
                    ReasonPhrase = "Forbidden.",
                    Content = new StringContent($"Your IP address was not valid. Expected: {session.UserHostAddress}, Actual: {System.Web.HttpContext.Current.Request.UserHostAddress}.")
                });
            }


            for (int i = 0; i < count; i++)
            {
                if (i >= 5 && !session.EnableMemberFunction) yield break;
                if (i >= 10 && !session.EnableVIPFunction) yield break;

                yield return _rnd.Next(1, 7);
            }
        }

        //// POST api/<controller>
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/<controller>/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/<controller>/5
        //public void Delete(int id)
        //{
        //}
    }
}