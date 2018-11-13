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
            }

            static public Path Pets => () => Root / "pets";

            static public Operation GetPets => () => Pets() <= (OperationType.Get, "Returns all pets from the system that the user has access to");
        }


    }
}
