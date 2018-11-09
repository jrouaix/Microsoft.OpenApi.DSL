using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.OpenApi.DSL
{
    public struct DslPathItem
    {
        public DslPathItem(DslDocument document, OpenApiPathItem pathItem)
        {
            Document = document;
            PathItem = pathItem;
        }

        public DslDocument Document { get; }
        public OpenApiPathItem PathItem { get; }


        public static DslOperation operator <=(DslPathItem pathItem, OperationType operationType) => pathItem.GetOperation(operationType);
        public static DslOperation operator <=(DslPathItem pathItem, (OperationType operationType, string description) properties) => pathItem.GetOperation(properties.operationType, properties.description);


        public static DslOperation operator >=(DslPathItem pathItem, OperationType operationType) => throw new NotImplementedException();
        public static DslOperation operator >=(DslPathItem pathItem, (OperationType operationType, string description) properties) => throw new NotImplementedException();
    }
}
