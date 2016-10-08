using Txt.Core;

namespace Txt.ABNF.Core.CRLF
{
    public class NewLineParserFactory : ParserFactory<NewLine, string>
    {
        public static NewLineParserFactory Default { get; } = new NewLineParserFactory();

        public override IParser<NewLine, string> Create()
        {
            return new NewLineParser();
        }
    }
}
