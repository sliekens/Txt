using Txt.Core;

namespace Txt.ABNF.Core.WSP
{
    public class WhiteSpaceParserFactory : ParserFactory<WhiteSpace, char>
    {
        public static WhiteSpaceParserFactory Default { get; } = new WhiteSpaceParserFactory();

        public override IParser<WhiteSpace, char> Create()
        {
            return new WhiteSpaceParser();
        }
    }
}
