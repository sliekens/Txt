// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BitLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    public class BitLexer : AlternativeLexer<Bit, Bit.Zero, Bit.One>
    {
        /// <summary>Initializes a new instance of the <see cref="BitLexer"/> class.</summary>
        public BitLexer()
            : base("BIT")
        {
        }

        protected override bool TryRead1(ITextScanner scanner, out Bit.Zero element)
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

        protected override bool TryRead2(ITextScanner scanner, out Bit.One element)
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