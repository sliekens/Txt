using Txt.Core;

namespace Txt.ABNF.Core.HTAB
{
    public class HorizontalTabParserFactory : ParserFactory<HorizontalTab, char>
    {
        public static HorizontalTabParserFactory Default { get; } = new HorizontalTabParserFactory();

        public override IParser<HorizontalTab, char> Create()
        {
            return new HorizontalTabParser();
        }
    }
}
