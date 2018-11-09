using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.OpenApi.Models;

namespace Microsoft.OpenApi.DSL.Tests
{
    public class DocumentDefinitionTests
    {
        public class MyDocument : DocumentDefinition<MyDocument>
        {
            public MyDocument() : base("1.0.0", "Swagger Petstore (Simple)")
            {
            }

            Path Pets => () => Root / "pets";

            Operation GetPets => () => Pets() <= (OperationType.Get, "Returns all pets from the system that the user has access to");
        }


    }
}
