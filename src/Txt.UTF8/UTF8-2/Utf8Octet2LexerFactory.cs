using System;
using System.Text;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;
using Txt.UTF8.UTF8_tail;

namespace Txt.UTF8.UTF8_2
{
    public class Utf8Octet2LexerFactory : ILexerFactory<Utf8Octet2>
    {
        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        private readonly ILexer<Utf8Tail> utf8TailLexer;

        private readonly IValueRangeLexerFactory valueRangeLexerFactory;

        public Utf8Octet2LexerFactory(
            [NotNull] IConcatenationLexerFactory concatenationLexerFactory,
            [NotNull] IValueRangeLexerFactory valueRangeLexerFactory,
            [NotNull] ILexer<Utf8Tail> utf8TailLexer)
        {
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
            this.concatenationLexerFactory = concatenationLexerFactory;
            this.valueRangeLexerFactory = valueRangeLexerFactory;
            this.utf8TailLexer = utf8TailLexer;
        }

        public ILexer<Utf8Octet2> Create()
        {
            return
                new Utf8Octet2Lexer(
                    concatenationLexerFactory.Create(
                        valueRangeLexerFactory.Create(0xC2, 0xDF, Encoding.UTF8),
                        utf8TailLexer));
        }
    }
}
