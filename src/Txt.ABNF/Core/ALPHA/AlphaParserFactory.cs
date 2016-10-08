using Txt.Core;

namespace Txt.ABNF.Core.ALPHA
{
    public class AlphaParserFactory : ParserFactory<Alpha, char>
    {
        public static AlphaParserFactory Default { get; } = new AlphaParserFactory();

        public override IParser<Alpha, char> Create()
        {
            return new AlphaParser();
        }
    }
}
