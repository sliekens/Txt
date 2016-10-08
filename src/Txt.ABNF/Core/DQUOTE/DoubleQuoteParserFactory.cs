using Txt.Core;

namespace Txt.ABNF.Core.DQUOTE
{
    public class DoubleQuoteParserFactory : ParserFactory<DoubleQuote, char>
    {
        public static DoubleQuoteParserFactory Default { get; } = new DoubleQuoteParserFactory();

        public override IParser<DoubleQuote, char> Create()
        {
            return new DoubleQuoteParser();
        }
    }
}
