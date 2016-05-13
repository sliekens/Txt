using System;
using System.Text;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;
using Txt.UTF8.UTF8_tail;

namespace Txt.UTF8.UTF8_3
{
    public class Utf8Octet3LexerFactory : ILexerFactory<Utf8Octet3>
    {
        private readonly IAlternationLexerFactory alternationLexerFactory;

        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        private readonly ILexer<Utf8Tail> utf8TailLexer;

        private readonly IValueRangeLexerFactory valueRangeLexerFactory;

        public Utf8Octet3LexerFactory(
            [NotNull] IAlternationLexerFactory alternationLexerFactory,
            [NotNull] IConcatenationLexerFactory concatenationLexerFactory,
            [NotNull] IValueRangeLexerFactory valueRangeLexerFactory,
            [NotNull] ILexer<Utf8Tail> utf8TailLexer)
        {
            if (alternationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternationLexerFactory));
            }
            if (concatenationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(concatenationLexerFactory));
            }
            if (valueRangeLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(valueRangeLexerFactory));
            }
            if (utf8TailLexer == null)
            {
                throw new ArgumentNullException(nameof(utf8TailLexer));
            }
            this.alternationLexerFactory = alternationLexerFactory;
            this.concatenationLexerFactory = concatenationLexerFactory;
            this.valueRangeLexerFactory = valueRangeLexerFactory;
            this.utf8TailLexer = utf8TailLexer;
        }

        public ILexer<Utf8Octet3> Create()
        {
            return
                new Utf8Octet3Lexer(
                    alternationLexerFactory.Create(
                        concatenationLexerFactory.Create(
                            valueRangeLexerFactory.Create(0xE0, 0xE0, Encoding.UTF8),
                            valueRangeLexerFactory.Create(0xA0, 0xBF, Encoding.UTF8),
                            utf8TailLexer),
                        concatenationLexerFactory.Create(
                            valueRangeLexerFactory.Create(0xE1, 0xEC, Encoding.UTF8),
                            utf8TailLexer,
                            utf8TailLexer),
                        concatenationLexerFactory.Create(
                            valueRangeLexerFactory.Create(0xED, 0xED, Encoding.UTF8),
                            valueRangeLexerFactory.Create(0x80, 0x9F, Encoding.UTF8),
                            utf8TailLexer),
                        concatenationLexerFactory.Create(
                            valueRangeLexerFactory.Create(0xEE, 0xEF, Encoding.UTF8),
                            utf8TailLexer,
                            utf8TailLexer)));
        }
    }
}
