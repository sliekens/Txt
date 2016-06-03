using Txt.ABNF;

namespace Sample1.factor
{
    public class Factor : Concatenation
    {
        public Factor(Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}
