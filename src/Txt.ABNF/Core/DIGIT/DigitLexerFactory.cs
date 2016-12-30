using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Txt.ABNF.Core.DIGIT
{
    public sealed class DigitLexerFactory : RuleLexerFactory<Digit>
    {
        static DigitLexerFactory()
        {
            Default = new DigitLexerFactory();
        }

        [NotNull]
        public static DigitLexerFactory Default { get; }

        public override ILexer<Digit> Create()
        {
            var innerLexer = ValueRange.Create('\x30', '\x39');
            return new DigitLexer(innerLexer);
        }
    }
}
