using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF
{
    /// <summary>
    ///     Wraps a collection of <see cref="ILexer" /> and tests their <see cref="ILexer.ReadElement" /> method until the
    ///     longest match is found.
    ///     This class implements a greedy algorithm. For a first-match-wins algorithm, use the
    ///     <see cref="AlternationLexer" /> class instead.
    /// </summary>
    public class GreedyAlternativeLexer : Lexer<Alternation>
    {
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly ILexer[] lexers;

        public GreedyAlternativeLexer([NotNull] [ItemNotNull] ILexer[] lexers)
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

        protected override ReadResult<Alternation> ReadImpl(ITextScanner scanner, ITextContext context)
        {
            ILexer bestCandidate = null;
            var bestCandidateLength = -1;
            var ordinal = 0;
            IList<SyntaxError> errors = new List<SyntaxError>(lexers.Length);
            SyntaxError partialMatch = null;

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
                    if (length != 0)
                    {
                        scanner.Unread(alternative.Text);
                    }
                }
                else
                {
                    errors.Add(candidate.Error);
                    if ((partialMatch == null) || (candidate.Text.Length > partialMatch.Text.Length))
                    {
                        partialMatch = candidate.Error;
                    }
                }
            }
            if (bestCandidate == null)
            {
                Debug.Assert(partialMatch != null, "partialMatch != null");
                return new ReadResult<Alternation>(partialMatch);
            }
            var readResult = bestCandidate.ReadElement(scanner);
            Debug.Assert(readResult.Success, "readResult.Success");
            return
                new ReadResult<Alternation>(new Alternation(readResult.Text, readResult.Element, context, ordinal));
        }
    }
}
