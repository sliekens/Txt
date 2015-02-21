// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Alternative`2.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Text.Scanning
{
    using System.Diagnostics.Contracts;

    /// <summary>TODO </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    public class Alternative<T1, T2>
        where T1 : Element where T2 : Element
    {
        /// <summary>TODO </summary>
        private readonly T1 element1;

        /// <summary>TODO </summary>
        private readonly T2 element2;

        /// <summary>TODO </summary>
        /// <param name="element">TODO </param>
        public Alternative(T1 element)
        {
            Contract.Requires(element != null);
            this.element1 = element;
        }

        /// <summary>TODO </summary>
        /// <param name="element">TODO </param>
        public Alternative(T2 element)
        {
            Contract.Requires(element != null);
            this.element2 = element;
        }

        /// <summary>TODO </summary>
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

        /// <summary>TODO </summary>
        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.element1 == null || this.element2 == null);
        }
    }
}