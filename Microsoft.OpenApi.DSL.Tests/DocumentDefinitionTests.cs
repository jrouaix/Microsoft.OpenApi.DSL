using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.OpenApi.Models;

namespace Microsoft.OpenApi.DSL.Tests
{
    public class DocumentDefinitionTests
    {
        public class MyApi : DocumentDefinition<MyApi>
        {
            public MyApi() : base("1.0.0", "Swagger Petstore (Simple)")
            {
                Root.Document.Servers.Add(new OpenApiServer() { Url = "http://petstore.swagger.io/api" });
            }

            public static Path Pets => () => Root / "pets";

            public static Operation GetPets => () => Pets() <= (OperationType.Get, "Returns all pets from the system that the user has access to");

            Response GetPets200 => () => GetPets() > (200, "OK");
        }
    }
}
