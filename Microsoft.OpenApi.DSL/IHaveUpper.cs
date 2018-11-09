using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.OpenApi.DSL
{
    interface IHaveUpper<TUpper>
    {
        TUpper _ { get; }
    }
}
