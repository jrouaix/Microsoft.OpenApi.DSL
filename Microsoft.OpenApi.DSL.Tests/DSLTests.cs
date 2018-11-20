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
            document.Document.Servers.Add(new OpenApiServer() { Url = "http://petstore.swagger.io/api" });
            var testPath = document / "pets";
            var getOperation = testPath
                <= (OperationType.Get, "Returns all pets from the system that the user has access to")
                    > (200, "OK")
                ;

            var inline = OutputApi(document.Document);
            var classDef = OutputApi(DocumentDefinitionTests.MyApi.Root.Document);
            var sample = OutputApi(SampleDocument);

            inline.ShouldBe(sample);
            classDef.ShouldBe(sample);
        }

        [Fact]
        public void SuperSyntaxTree()
        {
            var document = Document("1.0.0", "Swagger Petstore (Simple)");
            var testPath = document / "test";
            var _ = testPath <= (OperationType.Get, "Returns all pets from the system that the user has access to");
            _ = testPath <= (OperationType.Post, "Change something");
            _ = testPath <= (OperationType.Head, "Mind blown");

            _ = document / "Hello"
                <= (OperationType.Get, "Say hello")
                ;

            OutputApi(document.Document);
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
    }

}

