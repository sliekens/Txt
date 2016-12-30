using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.ALPHA
{
    /// <summary>Creates instances of the <see cref="AlphaLexer" /> class.</summary>
    public class AlphaLexerFactory : RuleLexerFactory<Alpha>
    {
        static AlphaLexerFactory()
        {
            Default = new AlphaLexerFactory();
        }

        [NotNull]
        public static AlphaLexerFactory Default { get; }

        /// <inheritdoc />
        public override ILexer<Alpha> Create()
        {
            var upperCaseValueRangeLexer = ValueRange.Create('\x41', '\x5A');
            var lowerCaseValueRangeLexer = ValueRange.Create('\x61', '\x7A');
            var innerLexer = Alternation.Create(
                upperCaseValueRangeLexer,
                lowerCaseValueRangeLexer);
            return new AlphaLexer(innerLexer);
        }
    }
}
