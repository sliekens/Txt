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

    public partial class HexadecimalDigitLexer
        : AlternativeLexer<HexadecimalDigit, Digit, HexadecimalDigit.A, HexadecimalDigit.B, HexadecimalDigit.C, HexadecimalDigit.D, HexadecimalDigit.E, HexadecimalDigit.F>
    {
        private readonly ILexer<Digit> digitLexer;
        private readonly ILexer<Element> aLexer;
        private readonly ILexer<Element> bLexer;
        private readonly ILexer<Element> cLexer;
        private readonly ILexer<Element> dLexer;
        private readonly ILexer<Element> eLexer;
        private readonly ILexer<Element> fLexer;

        public HexadecimalDigitLexer(ILexer<Digit> digitLexer, ILexer<Element> aLexer, ILexer<Element> bLexer, ILexer<Element> cLexer, ILexer<Element> dLexer, ILexer<Element> eLexer, ILexer<Element> fLexer)
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
            Element terminal;
            if (this.aLexer.TryRead(scanner, out terminal))
            {
                element = new HexadecimalDigit.A(terminal);
                return true;
            }

            element = default(HexadecimalDigit.A);
            return false;
        }

        protected override bool TryRead3(ITextScanner scanner, out HexadecimalDigit.B element)
        {
            Element terminal;
            if (this.bLexer.TryRead(scanner, out terminal))
            {
                element = new HexadecimalDigit.B(terminal);
                return true;
            }

            element = default(HexadecimalDigit.B);
            return false;
        }

        protected override bool TryRead4(ITextScanner scanner, out HexadecimalDigit.C element)
        {
            Element terminal;
            if (this.cLexer.TryRead(scanner, out terminal))
            {
                element = new HexadecimalDigit.C(terminal);
                return true;
            }

            element = default(HexadecimalDigit.C);
            return false;
        }

        protected override bool TryRead5(ITextScanner scanner, out HexadecimalDigit.D element)
        {
            Element terminal;
            if (this.dLexer.TryRead(scanner, out terminal))
            {
                element = new HexadecimalDigit.D(terminal);
                return true;
            }

            element = default(HexadecimalDigit.D);
            return false;
        }

        protected override bool TryRead6(ITextScanner scanner, out HexadecimalDigit.E element)
        {
            Element terminal;
            if (this.eLexer.TryRead(scanner, out terminal))
            {
                element = new HexadecimalDigit.E(terminal);
                return true;
            }

            element = default(HexadecimalDigit.E);
            return false;
        }

        protected override bool TryRead7(ITextScanner scanner, out HexadecimalDigit.F element)
        {
            Element terminal;
            if (this.fLexer.TryRead(scanner, out terminal))
            {
                element = new HexadecimalDigit.F(terminal);
                return true;
            }

            element = default(HexadecimalDigit.F);
            return false;
        }
    }

    public partial class HexadecimalDigitLexer
    {
        public class ALexer : StringLexer
        {
            public ALexer()
                : base("A")
            {
            }
        }
    }

    public partial class HexadecimalDigitLexer
    {
        public class BLexer : StringLexer
        {
            public BLexer()
                : base("B")
            {
            }
        }
    }

    public partial class HexadecimalDigitLexer
    {
        public class CLexer : StringLexer
        {
            public CLexer()
                : base("C")
            {
            }
        }
    }

    public partial class HexadecimalDigitLexer
    {
        public class DLexer : StringLexer
        {
            public DLexer()
                : base("D")
            {
            }
        }
    }

    public partial class HexadecimalDigitLexer
    {
        public class ELexer : StringLexer
        {
            public ELexer()
                : base("E")
            {
            }
        }
    }

    public partial class HexadecimalDigitLexer
    {
        public class FLexer : StringLexer
        {
            public FLexer()
                : base("F")
            {
            }
        }
    }
}