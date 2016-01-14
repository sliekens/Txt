namespace TextFx.ABNF
{
    using System;
    using System.Collections.Generic;
    using JetBrains.Annotations;

    public class Concatenation : Element
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="concatenation"></param>
        /// <exception cref="ArgumentNullException">The value of <paramref name="concatenation" /> is a null reference.</exception>
        public Concatenation([NotNull] Concatenation concatenation)
            : base(concatenation)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="elements"></param>
        /// <param name="context"></param>
        /// <exception cref="ArgumentNullException">The value of <paramref name="elements" /> or <paramref name="context"/> is a null reference.</exception>
        public Concatenation([NotNull] IList<Element> elements, [NotNull] ITextContext context)
            : base(elements, context)
        {
        }
    }
}
