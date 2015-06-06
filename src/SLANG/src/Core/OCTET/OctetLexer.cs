// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OctetLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SLANG.Core.OCTET
{
    using System;

    [RuleName("OCTET")]
    public class OctetLexer : Lexer<Octet>
    {
        private readonly ILexer<Terminal> octetValueRangeLexer;

        public OctetLexer(ILexer<Terminal> octetValueRangeLexer)
        {
            if (octetValueRangeLexer == null)
            {
                throw new ArgumentNullException("octetValueRangeLexer", "Precondition: octetValueRangeLexer != null");
            }

            this.octetValueRangeLexer = octetValueRangeLexer;
        }

        public override bool TryRead(ITextScanner scanner, out Octet element)
        {
            Terminal result;
            if (this.octetValueRangeLexer.TryRead(scanner, out result))
            {
                element = new Octet(result);
                return true;
            }

            element = default(Octet);
            return false;
        }
    }
}