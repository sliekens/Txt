// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BitLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   TODO
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    /// <summary>TODO </summary>
    public class BitLexer : Lexer<Bit>
    {
        /// <summary>Initializes a new instance of the <see cref="BitLexer"/> class.</summary>
        public BitLexer()
            : base("BIT")
        {
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, out Bit element)
        {
            if (scanner.EndOfInput)
            {
                element = default(Bit);
                return false;
            }

            var context = scanner.GetContext();
            if (scanner.TryMatch('\u0030'))
            {
                element = new Bit('\u0030', context);
                return true;
            }

            if (scanner.TryMatch('\u0031'))
            {
                element = new Bit('\u0031', context);
                return true;
            }

            element = default(Bit);
            return false;
        }
    }
}