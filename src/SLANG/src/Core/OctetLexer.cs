// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OctetLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    using System;

    public partial class OctetLexer : Lexer<Octet>
    {
        private readonly ILexer<Element> octetValueRangeLexer;

        public OctetLexer(ILexer<Element> octetValueRangeLexer)
            : base("OCTET")
        {
            if (octetValueRangeLexer == null)
            {
                throw new ArgumentNullException("octetValueRangeLexer", "Precondition: octetValueRangeLexer != null");
            }

            this.octetValueRangeLexer = octetValueRangeLexer;
        }

        public override bool TryRead(ITextScanner scanner, out Octet element)
        {
            Element value;
            if (this.octetValueRangeLexer.TryRead(scanner, out value))
            {
                element = new Octet(value);
                return true;
            }

            element = default(Octet);
            return false;
        }
    }

    public partial class OctetLexer
    {
        public class OctetValueRangeLexer : ValueRangeLexer
        {
            public OctetValueRangeLexer()
                : base('\x00', '\xFF')
            {
            }
        }
    }
}
