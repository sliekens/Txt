namespace TextFx.ABNF
{
    using System.Diagnostics;

    public class Alternative : Element
    {
        private readonly Element element;

        private readonly int ordinal;

        public Alternative(Alternative alternative)
            : base(alternative)
        {
            this.element = alternative.element;
            this.ordinal = alternative.ordinal;
        }

        public Alternative(Element element, int ordinal)
            : base(element)
        {
            this.element = element;
            this.ordinal = ordinal;
        }

        public Element Element
        {
            get
            {
                Debug.Assert(this.element != null);
                return this.element;
            }
        }

        public int Ordinal
        {
            get
            {
                Debug.Assert(this.ordinal > 0, "this.ordinal > 0");
                return this.ordinal;
            }
        }

        public override string GetWellFormedText()
        {
            return this.Element.GetWellFormedText();
        }
    }
}