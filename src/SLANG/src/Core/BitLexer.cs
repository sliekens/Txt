// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BitLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    using System;

    public partial class BitLexer : AlternativeLexer<Bit, Bit.Zero, Bit.One>
    {
        private readonly ILexer<Element> zeroBitTerminalLexer;

        private readonly ILexer<Element> oneBitTerminalLexer;

        public BitLexer(ILexer<Element> zeroBitTerminalLexer, ILexer<Element> oneBitTerminalLexer)
            : base("BIT")
        {
            if (zeroBitTerminalLexer == null)
            {
                throw new ArgumentNullException("zeroBitTerminalLexer", "Precondition: zeroBitTerminalLexer != null");
            }

            if (oneBitTerminalLexer == null)
            {
                throw new ArgumentNullException("oneBitTerminalLexer", "Precondition: oneBitTerminalLexer != null");
            }

            this.zeroBitTerminalLexer = zeroBitTerminalLexer;
            this.oneBitTerminalLexer = oneBitTerminalLexer;
        }

        protected override bool TryRead1(ITextScanner scanner, out Bit.Zero element)
        {
            Element value;
            if (this.zeroBitTerminalLexer.TryRead(scanner, out value))
            {
                element = new Bit.Zero(value);
                return true;
            }

            element = default(Bit.Zero);
            return false;
        }

        protected override bool TryRead2(ITextScanner scanner, out Bit.One element)
        {
            Element value;
            if (this.oneBitTerminalLexer.TryRead(scanner, out value))
            {
                element = new Bit.One(value);
                return true;
            }

            element = default(Bit.One);
            return false;
        }
    }

    public partial class BitLexer
    {
        public class ZeroBitTerminalLexer : StringLexer
        {
            public ZeroBitTerminalLexer()
                : base("0")
            {
            }
        }
    }

    public partial class BitLexer
    {
        public class OneBitTerminalLexer : StringLexer
        {
            public OneBitTerminalLexer()
                : base("1")
            {
            }
        }
    }
}