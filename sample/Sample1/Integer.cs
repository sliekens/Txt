using Txt.ABNF;

namespace Sample1
{
    using System.Numerics;
    

    public class Integer : Concatenation
    {
        public Integer(Concatenation concatenation)
            : base(concatenation)
        {
        }

        public int ToInt()
        {
            return int.Parse(this.Text);
        }

        public long ToLong()
        {
            return long.Parse(this.Text);
        }

        public BigInteger ToBigInteger()
        {
            return BigInteger.Parse(this.Text);
        }
    }
}
