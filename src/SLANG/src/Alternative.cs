namespace SLANG
{
    using System.Diagnostics;

    public class Alternative : Element
    {
        private readonly Element element;

        public Alternative(Element element)
            : base(element)
        {
            this.element = element;
        }

        public Element Element
        {
            get
            {
                Debug.Assert(this.element != null);
                return this.element;
            }
        }

        public override string GetWellFormedData()
        {
            return this.Element.GetWellFormedData();
        }
    }
}
