// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OctetLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TextFx.ABNF.Core
{
    using System;

    [RuleName("OCTET")]
    public class OctetLexer : Lexer<Octet>
    {
        private readonly ILexer<Terminal> innerLexer;

        public OctetLexer(ILexer<Terminal> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer");
            }

            this.innerLexer = innerLexer;
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, out Octet element)
        {
            Terminal result;
            if (this.innerLexer.TryRead(scanner, out result))
            {
                element = new Octet(result);
                return true;
            }

            element = default(Octet);
            return false;
        }
    }
}