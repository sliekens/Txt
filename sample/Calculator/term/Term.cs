using Txt.ABNF;

namespace Calculator.term
{
    public class Term : Concatenation
    {
        public Term(Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}
