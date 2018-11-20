using System;
using System.Collections;
using System.Linq;
using Microsoft.OpenApi.Models;

namespace Microsoft.OpenApi.DSL
{
    public class DslOperation : IHaveUpper<DslPathItem>
    {
        public DslOperation(DslPathItem pathItem, OpenApiOperation operation)
        {
            PathItem = pathItem;
            Operation = operation;
        }

        public DslPathItem _ => PathItem;
        public DslPathItem PathItem { get; }
        public OpenApiOperation Operation { get; }

        public IEnumerator GetEnumerator()
        {
            return Operation.Responses.Values.GetEnumerator();
        }

        public static DslResponse operator >(DslOperation operation, (int statusCode, string description) properties) 
            => operation.GetResponse(properties.statusCode.ToString(), properties.description);
        public static DslResponse operator <(DslOperation operation, (int statusCode, string description) properties)
            => operation > properties;

        //public static DslOperations operator <=(DslOperation operation, OperationType operationType)
        //    => new DslOperations(operation.PathItem, operation.PathItem <= operationType);
        //public static DslOperations operator >=(DslOperation operation, OperationType operationType)
        //    => operation <= operationType;

        //public static DslOperations operator <=(DslOperation operation, (OperationType operationType, string description) properties)
        //    => new DslOperations(operation.PathItem, operation.PathItem <= properties);
        //public static DslOperations operator >=(DslOperation operation, (OperationType operationType, string description) properties)
        //    => operation <= properties;
    }

    //public static class DslOperationExtensions
    //{
    //    public static void Add(this DslOperation operation, (string statusCode, string description) response)
    //        => operation.GetResponse(response);
    //}
}
