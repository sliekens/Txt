using Txt.ABNF;

namespace Sample1
{
    

    public class Sign : Alternation
    {
        public Sign(Alternation alternation)
            : base(alternation)
        {
        }
    }
}
