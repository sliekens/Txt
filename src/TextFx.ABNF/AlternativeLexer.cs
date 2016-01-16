namespace TextFx.ABNF
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using JetBrains.Annotations;

    /// <summary>
    ///     Wraps a collection of <see cref="ILexer" /> and tests their <see cref="ILexer.ReadElement" /> method until a
    ///     match is found. This class implements a first-match-wins algorithm. For a greedy algorithm, use the
    ///     <see cref="GreedyAlternativeLexer" /> class instead.
    /// </summary>
    public class AlternativeLexer : Lexer<Alternative>
    {
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly ILexer[] lexers;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lexers"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public AlternativeLexer([NotNull][ItemNotNull] params ILexer[] lexers)
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scanner"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        public override ReadResult<Alternative> Read(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var context = scanner.GetContext();
            IList<SyntaxError> errors = new List<SyntaxError>(lexers.Length);

            // ReSharper disable once ForCanBeConvertedToForeach
            for (var i = 0; i < lexers.Length; i++)
            {
                var result = lexers[i].ReadElement(scanner);
                if (!result.Success)
                {
                    errors.Add(result.Error);
                }
                else
                {
                    return ReadResult<Alternative>.FromResult(new Alternative(result.Element, i + 1));
                }
            }
            return ReadResult<Alternative>.FromError(
                new AggregateSyntaxError(errors)
                {
                    Message = "One or more syntax errors were found.",
                    Context = context
                });
        }
    }
}
