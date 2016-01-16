namespace Sample1
{
    using TextFx.ABNF;

    public class Sign : Alternative
    {
        public Sign(Alternative alternative)
            : base(alternative)
        {
        }
    }
}
