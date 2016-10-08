using Txt.Core;

namespace Txt.ABNF.Core.BIT
{
    public class BitParserFactory : ParserFactory<Bit, bool>
    {
        public static BitParserFactory Default { get; } = new BitParserFactory();

        public override IParser<Bit, bool> Create()
        {
            return new BitParser();
        }
    }
}
