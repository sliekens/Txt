using System;
using System.Text;
using Txt.ABNF;
using Txt.Core;
using Txt.UTF8.UTF8_tail;

namespace Txt.UTF8.UTF8_4
{
    public class Utf8Octet4LexerFactory : ILexerFactory<Utf8Octet4>
    {
        private readonly IAlternationLexerFactory alternationLexerFactory;

        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        private readonly ILexer<Utf8Tail> utf8TailLexer;

        private readonly IValueRangeLexerFactory valueRangeLexerFactory;

        public Utf8Octet4LexerFactory(
            IAlternationLexerFactory alternationLexerFactory,
            IConcatenationLexerFactory concatenationLexerFactory,
            IValueRangeLexerFactory valueRangeLexerFactory,
            ILexer<Utf8Tail> utf8TailLexer)
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

        public ILexer<Utf8Octet4> Create()
        {
            return
                new Utf8Octet4Lexer(
                    alternationLexerFactory.Create(
                        concatenationLexerFactory.Create(
                            valueRangeLexerFactory.Create(0xF0, 0xF0, Encoding.UTF8),
                            valueRangeLexerFactory.Create(0x90, 0xBF, Encoding.UTF8),
                            utf8TailLexer,
                            utf8TailLexer),
                        concatenationLexerFactory.Create(
                            valueRangeLexerFactory.Create(0xF1, 0xF3, Encoding.UTF8),
                            utf8TailLexer,
                            utf8TailLexer,
                            utf8TailLexer),
                        concatenationLexerFactory.Create(
                            valueRangeLexerFactory.Create(0xF4, 0xF4, Encoding.UTF8),
                            valueRangeLexerFactory.Create(0x80, 0x8F, Encoding.UTF8),
                            utf8TailLexer,
                            utf8TailLexer)));
        }
    }
}
