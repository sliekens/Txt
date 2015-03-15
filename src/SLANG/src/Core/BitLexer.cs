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
            if (scanner.TryMatch('\x30'))
            {
                element = new Bit('\x30', context);
                return true;
            }

            if (scanner.TryMatch('\x31'))
            {
                element = new Bit('\x31', context);
                return true;
            }

            element = default(Bit);
            return false;
        }
    }
}