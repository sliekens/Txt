using Txt.Core;

namespace Txt.ABNF.Core.CR
{
    public class CarriageReturnParserFactory : ParserFactory<CarriageReturn, char>
    {
        public static CarriageReturnParserFactory Default { get; } = new CarriageReturnParserFactory();

        public override IParser<CarriageReturn, char> Create()
        {
            return new CarriageReturnParser();
        }
    }
}
