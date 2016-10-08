using Txt.Core;

namespace Txt.ABNF.Core.SP
{
    public class SpaceParserFactory : ParserFactory<Space, char>
    {
        public static SpaceParserFactory Default { get; } = new SpaceParserFactory();

        public override IParser<Space, char> Create()
        {
            return new SpaceParser();
        }
    }
}
