namespace TextFx.ABNF
{
    using System;
    using JetBrains.Annotations;

    public class Alternative : Element
    {
        /// <summary>
        /// </summary>
        /// <param name="alternative"></param>
        /// <exception cref="ArgumentNullException">The value of <paramref name="alternative" /> is a null reference.</exception>
        public Alternative([NotNull] Alternative alternative)
            : base(alternative)
        {
            Ordinal = alternative.Ordinal;
            Element = alternative.Element;
        }

        /// <summary>
        /// </summary>
        /// <param name="sequence">The text in the alternative sequence.</param>
        /// <param name="alternative">The matching alternative.</param>
        /// <param name="context">An object that describes the current element's context.</param>
        /// <param name="ordinal">The ordinal position of the matching alternative.</param>
        /// <exception cref="ArgumentNullException">The value of <paramref name="alternative" /> is a null reference.</exception>
        public Alternative(
            [NotNull] string sequence,
            [NotNull] Element alternative,
            [NotNull] ITextContext context,
            int ordinal)
            : base(sequence, new[] {alternative}, context)
        {
            Element = alternative;
            Ordinal = ordinal;
        }

        public Element Element { get; }

        public int Ordinal { get; }
    }
}
