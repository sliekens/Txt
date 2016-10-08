using Txt.Core;

namespace Txt.ABNF.Core.OCTET
{
    public class OctetParserFactory : ParserFactory<Octet, byte[]>
    {
        public static OctetParserFactory Default { get; } = new OctetParserFactory();

        public override IParser<Octet, byte[]> Create()
        {
            return new OctetParser();
        }
    }
}
