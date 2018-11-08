using Microsoft.OpenApi.Models;

namespace Microsoft.OpenApi.DSL
{
    public class DslResponse
    {
        internal DslResponse(DslOperation operation, OpenApiResponse response)
        {
            Operation = operation;
            Response = response;
        }

        public DslOperation Operation { get; }
        public OpenApiResponse Response { get; }
    }
}
