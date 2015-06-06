namespace SLANG
{
    using System.Collections.Generic;

    public class Repetition : Sequence
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