using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF
{
    public class Repetition : Element
    {
        /// <summary>
        /// </summary>
        /// <param name="repetition"></param>
        /// <exception cref="ArgumentNullException">The value of <paramref name="repetition" /> is a null reference.</exception>
        public Repetition([NotNull] Repetition repetition)
            : base(repetition)
        {
        }

        /// <summary>
        /// </summary>
        /// <param name="repetition"></param>
        /// <param name="elements"></param>
        /// <param name="context"></param>
        /// <exception cref="ArgumentNullException">
        ///     The value of <paramref name="repetition" /> or <paramref name="elements" /> or
        ///     <paramref name="context" /> is a null reference.
        /// </exception>
        public Repetition(
            [NotNull] string repetition,
            [NotNull] IList<Element> elements,
            [NotNull] ITextContext context)
            : base(repetition, elements, context)
        {
        }
    }
}
