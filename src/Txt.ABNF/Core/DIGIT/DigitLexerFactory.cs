using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.DIGIT
{
    public sealed class DigitLexerFactory : LexerFactory<Digit>
    {
        static DigitLexerFactory()
        {
            Default = new DigitLexerFactory();
        }

        [NotNull]
        public static DigitLexerFactory Default { get; }

        public override ILexer<Digit> Create()
        {
            return new DigitLexer();
        }
    }
}
