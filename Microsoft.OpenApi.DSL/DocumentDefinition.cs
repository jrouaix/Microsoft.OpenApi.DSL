using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.OpenApi.Models;
using static Microsoft.OpenApi.DSL.DSL;

namespace Microsoft.OpenApi.DSL
{
    public delegate DslPathItem Path();

    public delegate DslOperation Operation();

    public delegate DslResponse Response();


    public class DocumentDefinition<TDocumentDefinition>
        where TDocumentDefinition : DocumentDefinition<TDocumentDefinition>, new()
    {
        private readonly DslDocument _root;

        public static DslDocument Root { get; private set; }

        public DocumentDefinition(string version, string title, string description = null, Uri termsOfService = null)
        {
            _root = Document(version, title, description, termsOfService);
            Root = _root;

            var properties = typeof(TDocumentDefinition)
                .GetProperties(BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic)
                .Select(p =>
                {
                    var getter = p.GetMethod;
                    var deleg = getter.IsStatic ? getter.Invoke(null, null) : getter.Invoke(this, null);

                    switch (getter.ReturnType)
                    {
                        case Type t when t == typeof(Path):
                            ((Path)deleg)();
                            break;
                        case Type t when t == typeof(Operation):
                            ((Operation)deleg)();
                            break;
                        case Type t when t == typeof(Response):
                            ((Response)deleg)();
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    return 1;
                }).ToArray();
        }

        static DocumentDefinition()
        {
            var init = new TDocumentDefinition();
        }
    }
}
