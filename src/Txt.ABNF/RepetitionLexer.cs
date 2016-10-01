using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF
{
    /// <summary>Provides the base class for lexers whose lexer rule is a repetition of elements.</summary>
    public class RepetitionLexer : Lexer<Repetition>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RepetitionLexer" /> class with a specified lower and upper bound,
        ///     both inclusive.
        /// </summary>
        /// <param name="lexer">The lexer for the repeating element.</param>
        /// <param name="lowerBound">A number that indicates the minimum number of occurrences (inclusive).</param>
        /// <param name="upperBound">A number that indicates the maximum number of occurrences (inclusive).</param>
        public RepetitionLexer([NotNull] ILexer<Element> lexer, int lowerBound, int upperBound)
        {
            if (lexer == null)
            {
                throw new ArgumentNullException(nameof(lexer));
            }
            if (lowerBound < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(lowerBound), "Precondition: lowerBound >= 0");
            }
            if (upperBound < lowerBound)
            {
                throw new ArgumentOutOfRangeException(nameof(upperBound), "Precondition: upperBound >= lowerBound");
            }
            Lexer = lexer;
            LowerBound = lowerBound;
            UpperBound = upperBound;
        }

        [NotNull]
        public ILexer<Element> Lexer { get; }

        public int LowerBound { get; }

        public int UpperBound { get; }

        protected override IEnumerable<Repetition> ReadImpl(ITextScanner scanner, ITextContext context)
        {
            var success = false;
            foreach (var repetition in Branch(scanner, context, new List<Element>(LowerBound)))
            {
                success = true;
                yield return repetition;
            }
            if (!success)
            {
                scanner.Seek(context.Offset);
            }
        }

        [NotNull]
        [ItemNotNull]
        private IEnumerable<Repetition> Branch(
            [NotNull] ITextScanner scanner,
            [NotNull] ITextContext root,
            [NotNull] List<Element> elements)
        {
            if (elements.Count == UpperBound)
            {
                yield return new Repetition(string.Concat(elements), elements, root);
            }
            else
            {
                var element = Lexer.Read(scanner);
                if (element != null)
                {
                    var copy = new List<Element>(elements) { element };
                    foreach (var repetition in Branch(scanner, root, copy))
                    {
                        yield return repetition;
                    }
                }
                else if (elements.Count >= LowerBound)
                {
                    yield return new Repetition(string.Concat(elements), elements, root);
                }
            }
        }
    }
}
