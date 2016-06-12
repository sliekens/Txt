using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF
{
    /// <summary>
    ///     Wraps a collection of <see cref="ILexer" /> and tests their <see cref="ILexer.ReadElement" /> method until a
    ///     match is found. This class implements a first-match-wins algorithm. For a greedy algorithm, use the
    ///     <see cref="GreedyAlternativeLexer" /> class instead.
    /// </summary>
    public class AlternationLexer : Lexer<Alternation>
    {
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly ILexer[] lexers;

        /// <summary>
        /// </summary>
        /// <param name="lexers"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public AlternationLexer([NotNull] [ItemNotNull] params ILexer[] lexers)
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
        /// </summary>
        /// <param name="scanner"></param>
        /// <param name="context"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        protected override ReadResult<Alternation> ReadImpl(ITextScanner scanner, ITextContext context)
        {
            IList<SyntaxError> errors = new List<SyntaxError>(lexers.Length);
            SyntaxError partialMatch = null;

            // ReSharper disable once ForCanBeConvertedToForeach
            for (var i = 0; i < lexers.Length; i++)
            {
                var result = lexers[i].ReadElement(scanner);
                if (result.Success)
                {
                    return
                        new ReadResult<Alternation>(new Alternation(result.Text, result.Element, context, i + 1));
                }
                errors.Add(result.Error);
                if ((partialMatch == null) || (result.Text.Length > partialMatch.Text.Length))
                {
                    partialMatch = result.Error;
                }
            }
            Debug.Assert(partialMatch != null, "partialMatch != null");
            return new ReadResult<Alternation>(partialMatch);
        }
    }
}
