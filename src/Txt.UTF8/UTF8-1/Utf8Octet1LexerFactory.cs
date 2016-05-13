using System;
using System.Text;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Txt.UTF8.UTF8_1
{
    public class Utf8Octet1LexerFactory : ILexerFactory<Utf8Octet1>
    {
        private readonly IValueRangeLexerFactory valueRangeLexerFactory;

        public Utf8Octet1LexerFactory([NotNull] IValueRangeLexerFactory valueRangeLexerFactory)
        {
            if (valueRangeLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(valueRangeLexerFactory));
            }
            this.valueRangeLexerFactory = valueRangeLexerFactory;
        }

        public ILexer<Utf8Octet1> Create()
        {
            return new Utf8Octet1Lexer(valueRangeLexerFactory.Create(0x0, 0x7F, Encoding.UTF8));
        }
    }
}
