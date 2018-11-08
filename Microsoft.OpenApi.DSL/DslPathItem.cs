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

        [Obsolete("Use the <= operator")]
        public DslOperation this[OperationType operationType] => this.GetOperation(operationType);

        public static DslOperation operator <=(DslPathItem pathItem, OperationType operationType)
            => pathItem.GetOperation(operationType);
        public static DslOperation operator >=(DslPathItem pathItem, OperationType operationType)
            => pathItem <= operationType;

        public static DslOperation operator <=(DslPathItem pathItem, (OperationType operationType, string description) properties)
            => pathItem.GetOperation(properties.operationType, properties.description);
        public static DslOperation operator >=(DslPathItem pathItem, (OperationType operationType, string description) properties)
            => pathItem <= properties;


    }
}
