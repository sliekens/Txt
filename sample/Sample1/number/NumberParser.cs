using Txt.Core;

namespace Sample1.number
{
    public class NumberParser : Parser<Number, int>
    {
        protected override int ParseImpl(Number number)
        {
            return int.Parse(number.Text);
        }
    }
}
