using Txt.ABNF;

namespace Sample1.term
{
    public class Term : Concatenation
    {
        public Term(Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}
