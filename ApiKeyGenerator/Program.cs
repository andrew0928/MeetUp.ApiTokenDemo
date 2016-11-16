using MeetUp.ApiTokenDemo.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiKeyGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                Console.WriteLine($"usage: ApiKeyGenerator.exe {{client id}} {{tag}} {{tag}} {{tag}} {{tag}}");
                return;
            }


            TokenHelper.AddKeyFile("APIKEY", @"E:\BlogWork\MeetUp.ApiTokenDemo\APIKEY-PRIVATE.xml");


            ApiKeyToken apikey = TokenHelper.CreateToken<ApiKeyToken>();

            apikey.ExpireDate = DateTime.Now.AddYears(1);
            apikey.ClientID = args[0];
            apikey.Tags = args.Skip(1).ToArray();

            Console.WriteLine("API KEY Generated:");
            Console.WriteLine(TokenHelper.EncodeToken("APIKEY", apikey));
        }
    }
}
