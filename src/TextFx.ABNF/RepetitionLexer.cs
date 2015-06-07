namespace TextFx.ABNF
{
    using System;
    using System.Collections.Generic;

    /// <summary>Provides the base class for lexers whose lexer rule is a repetition of elements.</summary>
    public class RepetitionLexer : Lexer<Repetition>
    {
        private readonly int lowerBound;

        private readonly ILexer repeatingElementLexer;

        private readonly int upperBound;

        /// <summary>Initializes a new instance of the <see cref="RepetitionLexer" /> class with a specified lower and upper bound, both inclusive.</summary>
        /// <param name="repeatingElementLexer">The lexer for the repeating element.</param>
        /// <param name="lowerBound">A number that indicates the minimum number of occurrences (inclusive).</param>
        /// <param name="upperBound">A number that indicates the maximum number of occurrences (inclusive).</param>
        public RepetitionLexer(ILexer repeatingElementLexer, int lowerBound, int upperBound)
        {
            if (repeatingElementLexer == null)
            {
                throw new ArgumentNullException("repeatingElementLexer");
            }

            if (lowerBound < 0)
            {
                throw new ArgumentOutOfRangeException("lowerBound", "Precondition: lowerBound >= 0");
            }

            if (upperBound < lowerBound)
            {
                throw new ArgumentOutOfRangeException("upperBound", "Precondition: upperBound >= lowerBound");
            }

            this.repeatingElementLexer = repeatingElementLexer;
            this.lowerBound = lowerBound;
            this.upperBound = upperBound;
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, out Repetition element)
        {
            if (scanner.EndOfInput && this.lowerBound != 0)
            {
                element = default(Repetition);
                return false;
            }

            var context = scanner.GetContext();
            var occurrences = new List<Element>(this.lowerBound);
            for (var i = 0; i < this.upperBound; i++)
            {
                Element occurrence;
                if (this.repeatingElementLexer.TryReadElement(scanner, out occurrence))
                {
                    occurrences.Add(occurrence);
                }
                else
                {
                    break;
                }
            }

            if (occurrences.Count < this.lowerBound)
            {
                if (occurrences.Count != 0)
                {
                    for (var i = occurrences.Count - 1; i >= 0; i--)
                    {
                        scanner.PutBack(occurrences[i].Values);
                    }
                }

                element = default(Repetition);
                return false;
            }

            element = new Repetition(occurrences, context);
            return true;
        }
    }
}