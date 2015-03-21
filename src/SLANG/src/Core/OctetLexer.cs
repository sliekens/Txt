// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OctetLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    public class OctetLexer : AlternativeLexer<Octet>
    {
        /// <summary>Initializes a new instance of the <see cref="OctetLexer"/> class.</summary>
        public OctetLexer()
            : base("OCTET", '\0', '\xFF')
        {
        }

        protected override Octet CreateInstance(char element, ITextContext context)
        {
            return new Octet(element, context);
        }
    }
}
