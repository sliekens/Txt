using Txt.Core;

namespace Calculator.number
{
    public class NumberParser : Parser<Number, double>
    {
        protected override double ParseImpl(Number number)
        {
            return double.Parse(number.Text);
        }
    }
}
