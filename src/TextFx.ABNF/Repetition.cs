namespace TextFx.ABNF
{
    using System.Collections.Generic;

    public class Repetition : Concatenation
    {
        public Repetition(Repetition repetition)
            : base(repetition)
        {
        }

        public Repetition(IList<Element> elements, ITextContext context)
            : base(elements, context)
        {
        }
    }
}