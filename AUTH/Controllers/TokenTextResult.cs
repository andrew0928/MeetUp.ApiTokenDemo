using MeetUp.ApiTokenDemo.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace MeetUp.ApiTokenDemo.AUTH.Controllers
{
    public class TokenTextResult : IHttpActionResult
    {
        private string _keyName = null;
        private TokenData _token = null;

        public TokenTextResult(string keyname, TokenData token)
        {
            this._keyName = keyname;
            this._token = token;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            HttpResponseMessage resp = new HttpResponseMessage()
            {
                Content = new StringContent(TokenHelper.EncodeToken(this._keyName, this._token))
            };

            return Task.FromResult(resp);
        }
    }
}