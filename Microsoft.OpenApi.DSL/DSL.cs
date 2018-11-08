using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.OpenApi.DSL
{
    public static class DSL
    {
        public static DslDocument Document(string version, string title, string description = null, Uri termsOfService = null)
        {
            return new DslDocument(
                new OpenApiDocument
                {
                    Info = new OpenApiInfo
                    {
                        Version = version,
                        Title = title,
                        Description = description,
                        TermsOfService = termsOfService,
                    },
                    Paths = new OpenApiPaths(),
                    Servers = new List<OpenApiServer>(),
                });
        }

        public static DslPathItem GetPath(this DslDocument document, string path, string separator = "/")
        {
            var fullPath = separator + path;

            if (!document.Document.Paths.TryGetValue(fullPath, out var pathItem))
            {
                pathItem = new OpenApiPathItem
                {
                    Operations = new Dictionary<OperationType, OpenApiOperation>(),
                };
                document.Document.Paths.Add(fullPath, pathItem);
            }

            return new DslPathItem(document, pathItem);
        }

        public static DslOperation GetOperation(this DslPathItem pathItem, OperationType operationType, string description = null)
        {
            if (!pathItem.PathItem.Operations.TryGetValue(operationType, out var operation))
            {
                operation = new OpenApiOperation
                {
                    Responses = new OpenApiResponses(),
                };
                pathItem.PathItem.Operations.Add(operationType, operation);
            };

            operation.Description = description;
            return new DslOperation(pathItem, operation);
        }

        public static DslResponse GetResponse(this DslOperation operation, string statusCode, string description = null)
        {
            if (!operation.Operation.Responses.TryGetValue(statusCode, out var response))
            {
                response = new OpenApiResponse
                {
                    Description = description,
                };
                operation.Operation.Responses.Add(statusCode, response);
            }

            response.Description = description;
            return new DslResponse(operation, response);
        }
    }
}

