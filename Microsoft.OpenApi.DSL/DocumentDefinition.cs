using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.OpenApi.Models;
using static Microsoft.OpenApi.DSL.DSL;

namespace Microsoft.OpenApi.DSL
{
    public delegate DslPathItem Path();

    public delegate DslOperation Operation();

    public class DocumentDefinition<TDocumentDefinition>
        where TDocumentDefinition : DocumentDefinition<TDocumentDefinition>
    {
        public DslDocument Root { get; }

        public DocumentDefinition(string version, string title, string description = null, Uri termsOfService = null)
        {
            Root = Document(version, title, description, termsOfService);
        }
    }
}
