// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OctetLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   TODO
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    /// <summary>TODO </summary>
    public class OctetLexer : Lexer<Octet>
    {
        /// <summary>Initializes a new instance of the <see cref="OctetLexer"/> class.</summary>
        public OctetLexer()
            : base("OCTET")
        {
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, out Octet element)
        {
            if (scanner.EndOfInput)
            {
                element = default(Octet);
                return false;
            }

            var context = scanner.GetContext();
            for (var c = '\u0000'; c <= '\u00FF'; c++)
            {
                if (scanner.TryMatch(c))
                {
                    element = new Octet(c, context);
                    return true;
                }
            }

            element = default(Octet);
            return false;
        }
    }
}