// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HexadecimalDigitLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    using System;
    using System.Diagnostics;

    public partial class HexadecimalDigitLexer
        : AlternativeLexer<HexadecimalDigit, Digit, HexadecimalDigit.A, HexadecimalDigit.B, HexadecimalDigit.C, HexadecimalDigit.D, HexadecimalDigit.E, HexadecimalDigit.F>
    {
        private readonly ILexer<Digit> digitLexer;
        private readonly ILexer<HexadecimalDigit.A> aLexer;
        private readonly ILexer<HexadecimalDigit.B> bLexer;
        private readonly ILexer<HexadecimalDigit.C> cLexer;
        private readonly ILexer<HexadecimalDigit.D> dLexer;
        private readonly ILexer<HexadecimalDigit.E> eLexer;
        private readonly ILexer<HexadecimalDigit.F> fLexer;



        public HexadecimalDigitLexer(ILexer<Digit> digitLexer, ILexer<HexadecimalDigit.A> aLexer, ILexer<HexadecimalDigit.B> bLexer, ILexer<HexadecimalDigit.C> cLexer, ILexer<HexadecimalDigit.D> dLexer, ILexer<HexadecimalDigit.E> eLexer, ILexer<HexadecimalDigit.F> fLexer)
            : base("HEXDIG")
        {
            if (digitLexer == null)
            {
                throw new ArgumentNullException("digitLexer", "Precondition: digitLexer != null");
            }

            if (bLexer == null)
            {
                throw new ArgumentNullException("bLexer", "Precondition: bLexer != null");
            }

            if (cLexer == null)
            {
                throw new ArgumentNullException("cLexer", "Precondition: cLexer != null");
            }

            if (dLexer == null)
            {
                throw new ArgumentNullException("dLexer", "Precondition: dLexer != null");
            }

            if (eLexer == null)
            {
                throw new ArgumentNullException("eLexer", "Precondition: eLexer != null");
            }

            this.digitLexer = digitLexer;
            this.aLexer = aLexer;
            this.bLexer = bLexer;
            this.cLexer = cLexer;
            this.dLexer = dLexer;
            this.eLexer = eLexer;
            this.fLexer = fLexer;
        }

        protected override bool TryRead1(ITextScanner scanner, out Digit element)
        {
            return this.digitLexer.TryRead(scanner, out element);
        }

        protected override bool TryRead2(ITextScanner scanner, out HexadecimalDigit.A element)
        {
            return this.aLexer.TryRead(scanner, out element);
        }

        protected override bool TryRead3(ITextScanner scanner, out HexadecimalDigit.B element)
        {
            return this.bLexer.TryRead(scanner, out element);
        }

        protected override bool TryRead4(ITextScanner scanner, out HexadecimalDigit.C element)
        {
            return this.cLexer.TryRead(scanner, out element);
        }

        protected override bool TryRead5(ITextScanner scanner, out HexadecimalDigit.D element)
        {
            return this.dLexer.TryRead(scanner, out element);
        }

        protected override bool TryRead6(ITextScanner scanner, out HexadecimalDigit.E element)
        {
            return this.eLexer.TryRead(scanner, out element);
        }

        protected override bool TryRead7(ITextScanner scanner, out HexadecimalDigit.F element)
        {
            return this.fLexer.TryRead(scanner, out element);
        }
    }

    public partial class HexadecimalDigitLexer
    {
        public class ALexer : Lexer<HexadecimalDigit.A>
        {
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