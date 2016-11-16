using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetUp.ApiTokenDemo.SDK
{
    public class SessionToken : TokenData
    {

        [JsonProperty]
        public DateTime CreateDate { get; set; }

        [JsonProperty]
        public string ClientID { get; set; }


        [JsonProperty]
        public bool EnableAdminFunction { get; set; }

        [JsonProperty]
        public bool EnableVIPFunction { get; set; }

        [JsonProperty]
        public bool EnableMemberFunction { get; set; }
    }
}
