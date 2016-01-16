namespace TextFx.ABNF
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using JetBrains.Annotations;

    /// <summary>
    ///     Wraps a collection of <see cref="ILexer" /> and tests their <see cref="ILexer.ReadElement" /> method until the
    ///     longest match is found.
    ///     This class implements a greedy algorithm. For a first-match-wins algorithm, use the
    ///     <see cref="AlternativeLexer" /> class instead.
    /// </summary>
    public class GreedyAlternativeLexer : Lexer<Alternative>
    {
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly ILexer[] lexers;

        public GreedyAlternativeLexer([NotNull][ItemNotNull] ILexer[] lexers)
        {
            if (lexers == null)
            {
                throw new ArgumentNullException(nameof(lexers));
            }
            if (lexers.Length == 0)
            {
                throw new ArgumentException("Argument is empty collection", nameof(lexers));
            }
            if (lexers.Contains(null))
            {
                throw new ArgumentException("Collection contains null", nameof(lexers));
            }
            this.lexers = lexers;
        }

        public override ReadResult<Alternative> Read(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var context = scanner.GetContext();
            ILexer bestCandidate = null;
            var bestCandidateLength = -1;
            var ordinal = 0;
            IList<SyntaxError> errors = new List<SyntaxError>(lexers.Length);

            // ReSharper disable once ForCanBeConvertedToForeach
            for (var i = 0; i < lexers.Length; i++)
            {
                var lexer = lexers[i];
                var candidate = lexer.ReadElement(scanner);
                if (candidate.Success)
                {
                    var alternative = candidate.Element;
                    var length = alternative.Text.Length;
                    if (length > bestCandidateLength)
                    {
                        bestCandidate = lexer;
                        bestCandidateLength = length;
                        ordinal = i + 1;
                    }
                    scanner.Unread(alternative.Text);
                }
                else
                {
                    errors.Add(candidate.Error);
                }
            }
            if (bestCandidate == null)
            {
                return ReadResult<Alternative>.FromError(
                    new AggregateSyntaxError(errors)
                    {
                        Message = "One or more syntax errors were found.",
                        Context = context
                    });
            }
            return ReadResult<Alternative>.FromResult(new Alternative(bestCandidate.ReadElement(scanner).Element, ordinal));
        }
    }
}
