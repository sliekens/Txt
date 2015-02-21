using System.Diagnostics.Contracts;

namespace Text.Scanning
{
    public class ElementMutex<T1, T2>
        where T1 : Element
        where T2 : Element
    {
        private readonly T1 element1;
        private readonly T2 element2;

        public ElementMutex(T1 element)
        {
            Contract.Requires(element != null);
            this.element1 = element;
        }

        public ElementMutex(T2 element)
        {
            Contract.Requires(element != null);
            this.element2 = element;
        }

        public Element Element
        {
            get { return this.element1 as Element ?? this.element2; }
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.element1 == null || this.element2 == null);
        }
    }
}