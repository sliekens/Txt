using System.Diagnostics.Contracts;

namespace Text.Scanning
{
    public class Alternative<T1, T2>
        where T1 : Element
        where T2 : Element
    {
        private readonly T1 element1;
        private readonly T2 element2;

        public Alternative(T1 element)
        {
            Contract.Requires(element != null);
            this.element1 = element;
        }

        public Alternative(T2 element)
        {
            Contract.Requires(element != null);
            this.element2 = element;
        }

        public Element Element
        {
            get
            {
                if (this.element1 != default(T1))
                {
                    return this.element1;
                }

                if (this.element2 != default(T2))
                {
                    return this.element2;
                }

                return default(Element);
            }
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.element1 == null || this.element2 == null);
        }
    }
}