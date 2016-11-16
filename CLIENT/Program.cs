using MeetUp.ApiTokenDemo.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CLIENT
{
    class Program
    {
        static void Main(string[] args)
        {
            string apikey = @"jgAAAAJDbGllbnRJRAAJAAAAQW5kcmV3V3UABFRhZ3MAJgAAAAIwAAQAAABWSVAAAjEABAAAAENUTwACMgAEAAAATVZQAAACVHlwZU5hbWUAJAAAAE1lZXRVcC5BcGlUb2tlbkRlbW8uU0RLLkFwaUtleVRva2VuAAlFeHBpcmVEYXRlALpyu8NfAQAAAA==|X56LXzXG3sAJgzEz7RSMGdcWBDroHNdu+6gpXluhoP0JZAxurzgpYPrwZ64ycCyIv0xiYoAjSj8Afz3CGW6HL1O/3N6c2as7OPNYUgOD6MGvHw5KXaZQ0WK4Y44TQn3kRzk7+55UlwMM2/ztSzM0o/XkL/wqstLwrTU3EHX/PeY=";


            HttpClient auth_client = new HttpClient();
            auth_client.BaseAddress = new Uri(@"http://localhost:63916/");
            auth_client.DefaultRequestHeaders.Add("X-APIKEY", apikey);


            HttpResponseMessage auth_msg = auth_client.PostAsync("/api/sessions", null).Result;
            string sessionTokenText = auth_msg.Content.ReadAsStringAsync().Result;
            Console.WriteLine("Get Session Token:");
            Console.WriteLine(sessionTokenText);



            HttpClient api_client = new HttpClient();
            api_client.BaseAddress = new Uri(@"http://localhost:6501/");
            api_client.DefaultRequestHeaders.Add("X-SESSION", sessionTokenText);

            Console.WriteLine("Get: /Hello");
            Console.WriteLine(api_client.GetAsync("/api/hello").Result.Content.ReadAsStringAsync().Result);
        }
    }
}
