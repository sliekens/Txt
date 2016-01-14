namespace TextFx.ABNF
{
    using System;
    using System.Collections.Generic;
    using JetBrains.Annotations;

    // TODO: this should derive from Element instead of Concatenation
    public class Repetition : Concatenation
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repetition"></param>
        /// <exception cref="ArgumentNullException">The value of <paramref name="repetition" /> is a null reference.</exception>
        public Repetition([NotNull] Repetition repetition)
            : base(repetition)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="elements"></param>
        /// <param name="context"></param>
        /// <exception cref="ArgumentNullException">The value of <paramref name="elements" /> or <paramref name="context"/> is a null reference.</exception>
        public Repetition([NotNull][ItemNotNull] IList<Element> elements, [NotNull] ITextContext context)
            : base(elements, context)
        {
        }
    }
}
