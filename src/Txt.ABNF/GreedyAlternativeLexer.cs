using System;
using System.Diagnostics;
using System.Linq;
using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF
{
    /// <summary>
    ///     Wraps a collection of <see cref="ILexer{TElement}" /> and tests their <see cref="ILexer{TElement}.Read" /> method
    ///     until the
    ///     longest match is found.
    ///     This class implements a greedy algorithm. For a first-match-wins algorithm, use the
    ///     <see cref="AlternationLexer" /> class instead.
    /// </summary>
    public class GreedyAlternativeLexer : Lexer<Alternation>
    {
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly ILexer<Element>[] lexers;

        public GreedyAlternativeLexer([NotNull] [ItemNotNull] ILexer<Element>[] lexers)
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

        protected override IReadResult<Alternation> ReadImpl(ITextScanner scanner, ITextContext context)
        {
            var offset = scanner.StartRecording();
            var greediestOffset = offset;
            Element greediest = null;
            var ordinal = 0;
            try
            {
                // ReSharper disable once ForCanBeConvertedToForeach
                for (var i = 0; i < lexers.Length; i++)
                {
                    var candidate = lexers[i].Read(scanner);
                    if (candidate.IsSuccess && (scanner.Offset > greediestOffset))
                    {
                        greediest = candidate.Element;
                        greediestOffset = scanner.Offset;
                        ordinal = i + 1;
                    }
                    scanner.Seek(offset);
                }
            }
            finally
            {
                scanner.StopRecording();
            }
            if (greediest == null)
            {
                return ReadResult<Alternation>.Fail(new SyntaxError(context, "No viable alternative"));
            }
            scanner.Seek(greediestOffset);
            var alternation = new Alternation(greediest.Text, greediest, context, ordinal);
            return ReadResult<Alternation>.Success(alternation);
        }
    }
}
