// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BitLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    using Microsoft.Practices.ServiceLocation;

    public partial class BitLexer : AlternativeLexer<Bit, Bit.Zero, Bit.One>
    {
        /// <summary>Initializes a new instance of the <see cref="BitLexer"/> class.</summary>
        public BitLexer(IServiceLocator serviceLocator)
            : base(serviceLocator, "BIT")
        {
        }
    }

    public partial class BitLexer
    {
        public class ZeroLexer : Lexer<Bit.Zero>
        {
            public ZeroLexer(IServiceLocator serviceLocator)
                : base(serviceLocator)
            {
            }

            public override bool TryRead(ITextScanner scanner, out Bit.Zero element)
            {
                Element terminal;
                if (!TryReadTerminal(scanner, "0", out terminal))
                {
                    element = default(Bit.Zero);
                    return false;
                }

                element = new Bit.Zero(terminal);
                return true;
            }
        }
    }

    public partial class BitLexer
    {
        public class OneLexer : Lexer<Bit.One>
        {
            public OneLexer(IServiceLocator serviceLocator)
                : base(serviceLocator)
            {
            }

            public override bool TryRead(ITextScanner scanner, out Bit.One element)
            {
                Element terminal;
                if (!TryReadTerminal(scanner, "1", out terminal))
                {
                    element = default(Bit.One);
                    return false;
                }

                element = new Bit.One(terminal);
                return true;
            }
        }
    }
}