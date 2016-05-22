using Txt.Core;

namespace Txt.ABNF.Core.CR
{
    public class CarriageReturnParser : Parser<CarriageReturn, char>
    {
        protected override char ParseImpl(CarriageReturn value)
        {
            return value.Text[0];
        }
    }
}
