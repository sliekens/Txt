using System;
using System.Diagnostics;
using System.Linq;
using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF
{
    /// <summary>
    ///     Wraps a collection of <see cref="ILexer{TElement}" /> and tests their <see cref="ILexer{TElement}.Read" /> method
    ///     until a
    ///     match is found. This class implements a first-match-wins algorithm. For a greedy algorithm, use the
    ///     <see cref="GreedyAlternativeLexer" /> class instead.
    /// </summary>
    public class AlternationLexer : Lexer<Alternation>
    {
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly ILexer<Element>[] lexers;

        /// <summary>
        /// </summary>
        /// <param name="lexers"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public AlternationLexer([NotNull] [ItemNotNull] params ILexer<Element>[] lexers)
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
        protected override IReadResult<Alternation> ReadImpl(ITextScanner scanner, ITextContext context)
        {
            // ReSharper disable once ForCanBeConvertedToForeach
            for (var i = 0; i < lexers.Length; i++)
            {
                var result = lexers[i].Read(scanner);
                if (result.IsSuccess)
                {
                    var alternation = new Alternation(result.Element.Text, result.Element, context, i + 1);
                    return ReadResult<Alternation>.Success(alternation);
                }
            }
            return ReadResult<Alternation>.Fail(new SyntaxError(context, "No viable alternative"));
        }
    }
}
