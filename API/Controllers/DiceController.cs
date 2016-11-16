using MeetUp.ApiTokenDemo.SDK;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MeetUp.ApiTokenDemo.API.Controllers
{
    public class DiceController : ApiController
    {
        private Random _rnd = new Random();


        // GET api/<controller>/5
        // 骰出指定次數的骰子
        [SwaggerOperation("GetMany")]
        [SwaggerOperationFilter(typeof(AddSessionTokenParameter))]
        public IEnumerable<int> Get(int count)
        {
            SessionToken session = TokenHelper.DecodeToken<SessionToken>("SESSION", this.Request.Headers.GetValues("X-SESSION").First());

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