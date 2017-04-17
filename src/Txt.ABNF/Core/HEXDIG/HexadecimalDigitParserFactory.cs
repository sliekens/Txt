using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.HEXDIG
{
    public class HexadecimalDigitParserFactory : ParserFactory<HexadecimalDigit, int>
    {
        static HexadecimalDigitParserFactory()
        {
            Default = new HexadecimalDigitParserFactory();
        }

        [NotNull]
        public static HexadecimalDigitParserFactory Default { get; }

        public override IParser<HexadecimalDigit, int> Create()
        {
            return new HexadecimalDigitParser();
        }
    }
}
