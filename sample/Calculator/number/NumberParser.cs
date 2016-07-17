using Txt.Core;

namespace Calculator.number
{
    public class NumberParser : Parser<Number, int>
    {
        protected override int ParseImpl(Number number)
        {
            return int.Parse(number.Text);
        }
    }
}
