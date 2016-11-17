using MeetUp.ApiTokenDemo.SDK;
using Newtonsoft.Json;
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
            TokenHelper.AddKeyFile("SESSION", @"..\..\..\SESSION.xml");


            //string apikey = @"jgAAAAJDbGllbnRJRAAJAAAAQW5kcmV3V3UABFRhZ3MAJgAAAAIwAAQAAABWSVAAAjEABAAAAENUTwACMgAEAAAATVZQAAACVHlwZU5hbWUAJAAAAE1lZXRVcC5BcGlUb2tlbkRlbW8uU0RLLkFwaUtleVRva2VuAAlFeHBpcmVEYXRlALpyu8NfAQAAAA==|X56LXzXG3sAJgzEz7RSMGdcWBDroHNdu+6gpXluhoP0JZAxurzgpYPrwZ64ycCyIv0xiYoAjSj8Afz3CGW6HL1O/3N6c2as7OPNYUgOD6MGvHw5KXaZQ0WK4Y44TQn3kRzk7+55UlwMM2/ztSzM0o/XkL/wqstLwrTU3EHX/PeY=";
            string apikey = @"eAAAAAJDbGllbnRJRAAJAAAAQW5kcmV3V3UABFRhZ3MAEAAAAAIwAAQAAABCQUQAAAJUeXBlTmFtZQAkAAAATWVldFVwLkFwaVRva2VuRGVtby5TREsuQXBpS2V5VG9rZW4ACUV4cGlyZURhdGUAzV+pxF8BAAAA|s6PTwIN0YmTN2DzQ9qQRLsKXaX7cYRvsV9CeR7ggGWZ6j4Rv+6KCI6WdfPQDCRcmkoDXkzIJ1ydmuLgTvTAUbcfJtkIzIf8Fx8IK/pkV4/78bKAPt0sUZqSyP5sFc4bbLiLfZSJL0e1pvAleNMda1vpc1KaJ4+CbJTw+hY1jcWo=";


            HttpClient auth_client = new HttpClient();
            //auth_client.BaseAddress = new Uri(@"http://localhost:63916/");
            auth_client.BaseAddress = new Uri(@"http://andrewmeetupdemoauth.azurewebsites.net/");
            auth_client.DefaultRequestHeaders.Add("X-APIKEY", apikey);


            HttpResponseMessage auth_msg = auth_client.PostAsync("/api/sessions", null).Result;
            string sessionTokenText = auth_msg.Content.ReadAsStringAsync().Result;
            Console.WriteLine("Get Session Token:");
            Console.WriteLine(sessionTokenText);

            // Q: 如何確認拿到的 session token 是安全的? (沒有被掉包)
            {
                //sessionTokenText = sessionTokenText.Substring(0, sessionTokenText.Length - 1) + "z";
                SessionToken st = TokenHelper.DecodeToken<SessionToken>("SESSION", sessionTokenText);
                Console.WriteLine("Session token verification PASS !!");
            }



            HttpClient api_client = new HttpClient();
            //api_client.BaseAddress = new Uri(@"http://localhost:6501/");
            api_client.BaseAddress = new Uri(@"http://andrewmeetupdemoapi.azurewebsites.net/"); ;
            api_client.DefaultRequestHeaders.Add("X-SESSION", sessionTokenText);

            Console.WriteLine("Get: /Hello");
            Console.WriteLine(api_client.GetAsync("/api/hello").Result.Content.ReadAsStringAsync().Result);

            Console.WriteLine("Dice 50 times:");
            int[] dice_results = JsonConvert.DeserializeObject<int[]>(api_client.GetAsync("/api/dice?count=50").Result.Content.ReadAsStringAsync().Result);
            Console.WriteLine($" - total: {dice_results.Length} times");
            Console.WriteLine($" - result: {string.Join(",", dice_results)}");
        }
    }
}
