using Microsoft.OpenApi.Extensions;
using Microsoft.OpenApi.Models;
using Shouldly;
using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using Microsoft.OpenApi.DSL;
using static Microsoft.OpenApi.DSL.DSL;

namespace Microsoft.OpenApi.DSL.Tests
{
    public class DSLTests
    {
        private readonly ITestOutputHelper _output;

        public DSLTests(ITestOutputHelper output)
        {
            _output = output;
        }

        OpenApiDocument SampleDocument => new OpenApiDocument
        {
            Info = new OpenApiInfo
            {
                Version = "1.0.0",
                Title = "Swagger Petstore (Simple)",
            },
            Servers = new List<OpenApiServer>
                {
                    new OpenApiServer { Url = "http://petstore.swagger.io/api" }
                },
            Paths = new OpenApiPaths
            {
                ["/pets"] = new OpenApiPathItem
                {
                    Operations = new Dictionary<OperationType, OpenApiOperation>
                    {
                        [OperationType.Get] = new OpenApiOperation
                        {
                            Description = "Returns all pets from the system that the user has access to",
                            Responses = new OpenApiResponses
                            {
                                ["200"] = new OpenApiResponse
                                {
                                    Description = "OK"
                                }
                            }
                        }
                    }
                }
            }
        };

        [Fact]
        public void GenerateOpenApiFirstExampleDocument()
        {
            var document = Document("1.0.0", "Swagger Petstore (Simple)");
            var testPath = document / "test";
            var getOperation = testPath
                <= (OperationType.Get, "Returns all pets from the system that the user has access to")
                ;

            OutputApi(document.Document);

            OutputApi(new DocumentDefinitionTests.MyDocument().Root.Document);

            OutputApi(SampleDocument);
        }

        private string OutputApi(OpenApiDocument api)
        {
            var output = api.Serialize(OpenApiSpecVersion.OpenApi3_0, OpenApiFormat.Yaml);
            _output.WriteLine(output);
            _output.WriteLine("");
            _output.WriteLine(new string('-', 50));
            _output.WriteLine("");
            return output;
        }


        [Fact]
        public void SuperSyntaxTree()
        {
            var document = Document("1.0.0", "Swagger Petstore (Simple)");
            var _ = document / "test"
                <= (OperationType.Get, "Returns all pets from the system that the user has access to")
                <= (OperationType.Post, "Change something")
                <= (OperationType.Head, "Mind blown")
                ;

            var __ = document / "Hello"
                <= (OperationType.Get, "Say hello")
                ;


            var outputString = document.Document.Serialize(OpenApiSpecVersion.OpenApi3_0, OpenApiFormat.Yaml);
            _output.WriteLine(outputString);
        }
    }

}

