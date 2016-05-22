using Txt.Core;

namespace Txt.ABNF.Core.OCTET
{
    /// <summary>Creates instances of the <see cref="OctetLexer" /> class.</summary>
    public class OctetLexerFactory : ILexerFactory<Octet>
    {
        /// <inheritdoc />
        public ILexer<Octet> Create()
        {
            return new OctetLexer();
        }
    }
}
