using Txt.Core;

namespace Txt.ABNF.Core.LF
{
    public class LineFeedParserFactory : ParserFactory<LineFeed, char>
    {
        public static LineFeedParserFactory Default { get; } = new LineFeedParserFactory();

        public override IParser<LineFeed, char> Create()
        {
            return new LineFeedParser();
        }
    }
}
