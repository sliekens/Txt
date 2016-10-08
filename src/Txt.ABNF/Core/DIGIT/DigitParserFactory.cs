using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.DIGIT
{
    public class DigitParserFactory : ParserFactory<Digit, int>
    {
        [NotNull]
        public static DigitParserFactory Default { get; } = new DigitParserFactory();

        public override IParser<Digit, int> Create()
        {
            return new DigitParser();
        }
    }
}
