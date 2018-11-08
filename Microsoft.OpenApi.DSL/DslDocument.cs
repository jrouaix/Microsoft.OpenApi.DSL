using Microsoft.OpenApi.Models;
using System;

namespace Microsoft.OpenApi.DSL
{
    public struct DslDocument
    {
        internal DslDocument(OpenApiDocument document)
        {
            Document = document;
        }

        public OpenApiDocument Document { get; }

        public static DslPathItem operator /(DslDocument document, string path) => document.GetPath(path);
    }
}
