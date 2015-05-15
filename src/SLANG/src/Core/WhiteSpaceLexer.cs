// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WhiteSpaceLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    using Microsoft.Practices.ServiceLocation;

    public class WhiteSpaceLexer : AlternativeLexer<WhiteSpace, Space, HorizontalTab>
    {
        public WhiteSpaceLexer(IServiceLocator serviceLocator)
            : base(serviceLocator, "WSP")
        {
        }
    }
}