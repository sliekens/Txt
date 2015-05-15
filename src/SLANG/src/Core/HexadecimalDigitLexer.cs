// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HexadecimalDigitLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    using Microsoft.Practices.ServiceLocation;

    public partial class HexadecimalDigitLexer : AlternativeLexer<HexadecimalDigit, Digit, HexadecimalDigit.A, HexadecimalDigit.B, HexadecimalDigit.C, HexadecimalDigit.D, HexadecimalDigit.E, HexadecimalDigit.F>
    {
        public HexadecimalDigitLexer(IServiceLocator serviceLocator)
            : base(serviceLocator, "HEXDIG")
        {   
        }
    }

    public partial class HexadecimalDigitLexer
    {
        public class ALexer : Lexer<HexadecimalDigit.A>
        {
            public ALexer(IServiceLocator serviceLocator)
                : base(serviceLocator)
            {
            }

            public override bool TryRead(ITextScanner scanner, out HexadecimalDigit.A element)
            {
                Element terminal;
                if (!TryReadTerminal(scanner, "A", out terminal))
                {
                    element = default(HexadecimalDigit.A);
                    return false;
                }

                element = new HexadecimalDigit.A(terminal);
                return true;
            }
        }
    }

    public partial class HexadecimalDigitLexer
    {
        public class BLexer : Lexer<HexadecimalDigit.B>
        {
            public BLexer(IServiceLocator serviceLocator)
                : base(serviceLocator)
            {
            }

            public override bool TryRead(ITextScanner scanner, out HexadecimalDigit.B element)
            {
                Element terminal;
                if (!TryReadTerminal(scanner, "B", out terminal))
                {
                    element = default(HexadecimalDigit.B);
                    return false;
                }

                element = new HexadecimalDigit.B(terminal);
                return true;
            }
        }
    }

    public partial class HexadecimalDigitLexer
    {
        public class CLexer : Lexer<HexadecimalDigit.C>
        {
            public CLexer(IServiceLocator serviceLocator)
                : base(serviceLocator)
            {
            }

            public override bool TryRead(ITextScanner scanner, out HexadecimalDigit.C element)
            {
                Element terminal;
                if (!TryReadTerminal(scanner, "C", out terminal))
                {
                    element = default(HexadecimalDigit.C);
                    return false;
                }

                element = new HexadecimalDigit.C(terminal);
                return true;
            }
        }
    }

    public partial class HexadecimalDigitLexer
    {
        public class DLexer : Lexer<HexadecimalDigit.D>
        {
            public DLexer(IServiceLocator serviceLocator)
                : base(serviceLocator)
            {
            }

            public override bool TryRead(ITextScanner scanner, out HexadecimalDigit.D element)
            {
                Element terminal;
                if (!TryReadTerminal(scanner, "D", out terminal))
                {
                    element = default(HexadecimalDigit.D);
                    return false;
                }

                element = new HexadecimalDigit.D(terminal);
                return true;
            }
        }
    }

    public partial class HexadecimalDigitLexer
    {
        public class ELexer : Lexer<HexadecimalDigit.E>
        {
            public ELexer(IServiceLocator serviceLocator)
                : base(serviceLocator)
            {
            }

            public override bool TryRead(ITextScanner scanner, out HexadecimalDigit.E element)
            {
                Element terminal;
                if (!TryReadTerminal(scanner, "E", out terminal))
                {
                    element = default(HexadecimalDigit.E);
                    return false;
                }

                element = new HexadecimalDigit.E(terminal);
                return true;
            }
        }
    }

    public partial class HexadecimalDigitLexer
    {
        public class FLexer : Lexer<HexadecimalDigit.F>
        {
            public FLexer(IServiceLocator serviceLocator)
                : base(serviceLocator)
            {
            }

            public override bool TryRead(ITextScanner scanner, out HexadecimalDigit.F element)
            {
                Element terminal;
                if (!TryReadTerminal(scanner, "F", out terminal))
                {
                    element = default(HexadecimalDigit.F);
                    return false;
                }

                element = new HexadecimalDigit.F(terminal);
                return true;
            }
        }
    }
}