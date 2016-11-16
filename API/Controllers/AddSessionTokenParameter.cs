using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Description;

namespace MeetUp.ApiTokenDemo.API.Controllers
{
    public class AddSessionTokenParameter : IOperationFilter
    {
        
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            if (operation.parameters == null)
            {
                operation.parameters = new List<Parameter>();
            }

            operation.parameters.Add(new Parameter
            {
                @default = "SessionToken in HTTP Headers",
                name = "X-SESSION",
                @in = "header",
                type = "string",
                required = true
            });
        }
    }
}