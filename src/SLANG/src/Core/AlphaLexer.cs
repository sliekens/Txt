// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AlphaLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    using Microsoft.Practices.ServiceLocation;

    public partial class AlphaLexer : AlternativeLexer<Alpha, Alpha.Uppercase, Alpha.Lowercase>
    {
        public AlphaLexer(IServiceLocator serviceLocator)
            : base(serviceLocator, "ALPHA")
        {
        }
    }

    public partial class AlphaLexer
    {
        public class UpperCaseLexer : AlternativeLexer<Alpha.Uppercase>
        {
            public UpperCaseLexer(IServiceLocator serviceLocator)
                : base(serviceLocator, '\x41', '\x5A')
            {
            }
        }
    }

    public partial class AlphaLexer
    {
        public class LowerCaseLexer : AlternativeLexer<Alpha.Lowercase>
        {
            public LowerCaseLexer(IServiceLocator serviceLocator)
                : base(serviceLocator, '\x61', '\x7A')
            {
            }
        }
    }
}