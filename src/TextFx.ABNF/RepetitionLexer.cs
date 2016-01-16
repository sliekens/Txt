namespace TextFx.ABNF
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Text;
    using JetBrains.Annotations;

    /// <summary>Provides the base class for lexers whose lexer rule is a repetition of elements.</summary>
    public class RepetitionLexer : Lexer<Repetition>
    {
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly int lowerBound;

        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly ILexer repeatingElementLexer;

        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly int upperBound;

        /// <summary>
        ///     Initializes a new instance of the <see cref="RepetitionLexer" /> class with a specified lower and upper bound,
        ///     both inclusive.
        /// </summary>
        /// <param name="repeatingElementLexer">The lexer for the repeating element.</param>
        /// <param name="lowerBound">A number that indicates the minimum number of occurrences (inclusive).</param>
        /// <param name="upperBound">A number that indicates the maximum number of occurrences (inclusive).</param>
        public RepetitionLexer([NotNull] ILexer repeatingElementLexer, int lowerBound, int upperBound)
        {
            if (repeatingElementLexer == null)
            {
                throw new ArgumentNullException(nameof(repeatingElementLexer));
            }
            if (lowerBound < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(lowerBound), "Precondition: lowerBound >= 0");
            }
            if (upperBound < lowerBound)
            {
                throw new ArgumentOutOfRangeException(nameof(upperBound), "Precondition: upperBound >= lowerBound");
            }
            this.repeatingElementLexer = repeatingElementLexer;
            this.lowerBound = lowerBound;
            this.upperBound = upperBound;
        }

        public override ReadResult<Repetition> Read(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var stringBuilder = new StringBuilder();
            var context = scanner.GetContext();
            IList<Element> elements = new List<Element>(lowerBound);

            // ReSharper disable once ForCanBeConvertedToForeach
            for (var i = 0; i < upperBound; i++)
            {
                var readResult = repeatingElementLexer.ReadElement(scanner);
                if (readResult.Success)
                {
                    elements.Add(readResult.Element);
                    stringBuilder.Append(readResult.Text);
                }
                else
                {
                    if (elements.Count >= lowerBound)
                    {
                        return
                            ReadResult<Repetition>.FromResult(
                                new Repetition(stringBuilder.ToString(), elements, context));
                    }
                    var partialMatch = stringBuilder.ToString();
                    if (partialMatch.Length != 0)
                    {
                        scanner.Unread(partialMatch);
                    }
                    return
                        ReadResult<Repetition>.FromSyntaxError(
                            new SyntaxError(
                                readResult.EndOfInput,
                                partialMatch,
                                readResult.ErrorText,
                                context,
                                readResult.Error));
                }
            }
            return ReadResult<Repetition>.FromResult(new Repetition(stringBuilder.ToString(), elements, context));
        }
    }
}
