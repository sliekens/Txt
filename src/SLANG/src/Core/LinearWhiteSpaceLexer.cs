// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LinearWhiteSpaceLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    using Microsoft.Practices.ServiceLocation;

    public partial class LinearWhiteSpaceLexer : RepetitionLexer<LinearWhiteSpace, LinearWhiteSpace.MultiLineWhiteSpace>
    {
        public LinearWhiteSpaceLexer(IServiceLocator serviceLocator)
            : base(serviceLocator, "LWSP", 0, int.MaxValue)
        {
        }
    }

    public partial class LinearWhiteSpaceLexer
    {
        public partial class MultiLineWhiteSpaceLexer : AlternativeLexer<LinearWhiteSpace.MultiLineWhiteSpace, WhiteSpace, LinearWhiteSpace.MultiLineWhiteSpace.NewLineWhiteSpace>
        {
            public MultiLineWhiteSpaceLexer(IServiceLocator serviceLocator)
                : base(serviceLocator)
            {
            }
        }
    }

    public partial class LinearWhiteSpaceLexer
    {
        public partial class MultiLineWhiteSpaceLexer
        {
            public class NewLineWhiteSpaceLexer :
                SequenceLexer<LinearWhiteSpace.MultiLineWhiteSpace.NewLineWhiteSpace, EndOfLine, WhiteSpace>
            {
                public NewLineWhiteSpaceLexer(IServiceLocator serviceLocator)
                    : base(serviceLocator)
                {
                }
            }
        }
    }
}