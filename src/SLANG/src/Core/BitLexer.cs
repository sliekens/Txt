// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BitLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    public class BitLexer : AlternativeLexer<Bit, Element, Element>
    {
        /// <summary>Initializes a new instance of the <see cref="BitLexer"/> class.</summary>
        public BitLexer()
            : base("BIT")
        {
        }

        protected override Bit CreateInstance1(Element element, ITextContext context)
        {
            return new Bit(element, 1, context);
        }

        protected override Bit CreateInstance2(Element element, ITextContext context)
        {
            return new Bit(element, 2, context);
        }

        protected override bool TryRead1(ITextScanner scanner, out Element element)
        {
            return TryReadTerminal(scanner, "0", out element);
        }

        protected override bool TryRead2(ITextScanner scanner, out Element element)
        {
            return TryReadTerminal(scanner, "1", out element);
        }
    }
}