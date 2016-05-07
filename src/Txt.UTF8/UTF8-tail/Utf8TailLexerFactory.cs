using System;
using System.Text;
using Txt.ABNF;
using Txt.Core;

namespace Txt.UTF8.UTF8_tail
{
    public class Utf8TailLexerFactory : ILexerFactory<Utf8Tail>
    {
        private readonly IValueRangeLexerFactory valueRangeLexerFactory;

        public Utf8TailLexerFactory(IValueRangeLexerFactory valueRangeLexerFactory)
        {
            if (valueRangeLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(valueRangeLexerFactory));
            }
            this.valueRangeLexerFactory = valueRangeLexerFactory;
        }

        public ILexer<Utf8Tail> Create()
        {
            return new Utf8TailLexer(valueRangeLexerFactory.Create(0x80, 0xBF, Encoding.UTF8));
        }
    }
}
