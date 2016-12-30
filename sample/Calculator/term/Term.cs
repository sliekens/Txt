using Txt.ABNF;
using Txt.Core;

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
