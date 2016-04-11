using Txt.ABNF;

namespace Sample1
{
    

    public class Sign : Alternative
    {
        public Sign(Alternative alternative)
            : base(alternative)
        {
        }
    }
}
