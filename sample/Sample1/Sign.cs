namespace Sample1
{
    using Text.ABNF;

    public class Sign : Alternative
    {
        public Sign(Alternative alternative)
            : base(alternative)
        {
        }
    }
}
