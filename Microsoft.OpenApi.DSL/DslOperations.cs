using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Microsoft.OpenApi.Models;

namespace Microsoft.OpenApi.DSL
{
    public struct DslOperations : IEnumerable<DslOperation>
    {
        public DslPathItem PathItem { get; }

        List<DslOperation> _list;

        public DslOperations(DslPathItem pathIdem, DslOperation firstOperation)
        {
            PathItem = pathIdem;
            _list = new List<DslOperation>
            {
                firstOperation
            };
        }

        internal void AddOperation(DslOperation operation) => _list.Add(operation);


        public IEnumerator<DslOperation> GetEnumerator() => _list.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => _list.GetEnumerator();

        public static DslOperations operator <=(DslOperations operations, OperationType operationType)
            => new DslOperations(operations.PathItem, operations.PathItem <= operationType);
        public static DslOperations operator >=(DslOperations operations, OperationType operationType)
            => operations <= operationType;

        public static DslOperations operator <=(DslOperations operations, (OperationType operationType, string description) properties)
            => new DslOperations(operations.PathItem, operations.PathItem <= properties);
        public static DslOperations operator >=(DslOperations operations, (OperationType operationType, string description) properties)
            => operations <= properties;
    }
}
