using Txt.ABNF;

namespace Sample1.number
{
    public class Number : Repetition
    {
        public Number(Repetition repetition)
            : base(repetition)
        {
        }

        public int GetValue()
        {
            return int.Parse(Text);
        }
    }
}
