using System.Collections.Generic;

namespace TextFx.ABNF
{
    using System;
    using System.Diagnostics;

    /// <summary>
    ///     Wraps a collection of <see cref="ILexer" /> and tests their <see cref="ILexer.ReadElement" /> method until a
    ///     match is found. This class implements a first-match-wins algorithm. For a greedy algorithm, use the <see cref="GreedyAlternativeLexer"/> class instead.
    /// </summary>
    public class AlternativeLexer : Lexer<Alternative>
    {
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly ILexer[] lexers;

        public AlternativeLexer(params ILexer[] lexers)
        {
            if (lexers == null)
            {
                throw new ArgumentNullException(nameof(lexers));
            }

            if (lexers.Length == 0)
            {
                throw new ArgumentException("Precondition: lexers.Count > 0", nameof(lexers));
            }

            // ReSharper disable once ForCanBeConvertedToForeach
            // ReSharper disable once LoopCanBeConvertedToQuery
            for (var i = 0; i < lexers.Length; i++)
            {
                if (lexers[i] == null)
                {
                    throw new ArgumentException("Precondition: lexers.All(lexer => lexer != null", nameof(lexers));
                }
            }

            this.lexers = lexers;
        }

        public override ReadResult<Alternative> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }

            var context = scanner.GetContext();
            IList<SyntaxError> errors = new List<SyntaxError>(this.lexers.Length);

            // ReSharper disable once ForCanBeConvertedToForeach
            for (var i = 0; i < this.lexers.Length; i++)
            {
                var result = this.lexers[i].ReadElement(scanner, null);
                if (!result.Success)
                {
                    errors.Add(result.Error);
                }
                else
                {
                    var element = new Alternative(new List<Element>(1) { result.Element }, context, i + 1);
                    if (previousElementOrNull != null)
                    {
                        element.PreviousElement = previousElementOrNull;
                        previousElementOrNull.NextElement = element;
                    }

                    return ReadResult<Alternative>.FromResult(element);
                }
            }

            return ReadResult<Alternative>.FromError(new AggregateSyntaxError(errors)
            {
                Message = "One or more syntax errors were found.",
                Context = context
            });
        }
    }
}