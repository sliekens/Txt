using System;
using Jetbrains.Annotations;

namespace Txt.ABNF
{
    public class Alternation : Element
    {
        /// <summary>
        /// </summary>
        /// <param name="alternation"></param>
        /// <exception cref="ArgumentNullException">The value of <paramref name="alternation" /> is a null reference.</exception>
        public Alternation([NotNull] Alternation alternation)
            : base(alternation)
        {
            Ordinal = alternation.Ordinal;
            Element = alternation.Element;
        }

        /// <summary>
        /// </summary>
        /// <param name="sequence">The text in the Alternationon.</param>
        /// <param name="alternative">The matched Alternation.</param>
        /// <param name="context">An object that describes the current element's context.</param>
        /// <param name="ordinal">The ordinal position of the matching Alternation.</param>
        /// <exception cref="ArgumentNullException">The value of <paramref name="alternative" /> is a null reference.</exception>
        public Alternation(
            [NotNull] string sequence,
            [NotNull] Element alternative,
            [NotNull] ITextContext context,
            int ordinal)
            : base(sequence, new[] { alternative }, context)
        {
            Element = alternative;
            Ordinal = ordinal;
        }

        public Element Element { get; }

        public int Ordinal { get; }
    }
}
