using Txt.Core;

namespace Txt.ABNF.Core.LWSP
{
    public class LinearWhiteSpaceParserFactory : ParserFactory<LinearWhiteSpace, string>
    {
        public static LinearWhiteSpaceParserFactory Default { get; } = new LinearWhiteSpaceParserFactory();

        public override IParser<LinearWhiteSpace, string> Create()
        {
            return new LinearWhiteSpaceParser();
        }
    }
}
