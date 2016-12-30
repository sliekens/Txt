using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.OCTET
{
    /// <summary>Creates instances of the <see cref="OctetLexer" /> class.</summary>
    public class OctetLexerFactory : LexerFactory<Octet>
    {
        static OctetLexerFactory()
        {
            Default = new OctetLexerFactory();
        }

        [NotNull]
        public static OctetLexerFactory Default { get; }

        /// <inheritdoc />
        public override ILexer<Octet> Create()
        {
            return new OctetLexer();
        }
    }
}
