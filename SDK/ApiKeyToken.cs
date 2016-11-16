using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetUp.ApiTokenDemo.SDK
{
    public class ApiKeyToken : TokenData
    {
        [JsonProperty]
        public string ClientID { get; set; }

        [JsonProperty]
        public string[] Tags { get; set; }

        public override bool IsValidate()
        {
            if (DateTime.Now > this.ExpireDate) return false;

            return base.IsValidate();
        }
    }
}
