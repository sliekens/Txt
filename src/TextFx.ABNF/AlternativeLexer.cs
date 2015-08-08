namespace TextFx.ABNF
{
    using System;

    /// <summary>
    ///     Wraps a collection of <see cref="ILexer" /> and tests their <see cref="ILexer.TryReadElement" /> method until a
    ///     match is found. This class implements a first-match-wins algorithm. For a greedy algorithm, use the <see cref="GreedyAlternativeLexer"/> class instead.
    /// </summary>
    public class AlternativeLexer : Lexer<Alternative>
    {
        private readonly ILexer[] lexers;

        public AlternativeLexer(params ILexer[] lexers)
        {
            if (lexers == null)
            {
                throw new ArgumentNullException("lexers");
            }

            if (lexers.Length == 0)
            {
                throw new ArgumentException("Precondition: lexers.Count > 0", "lexers");
            }

            // ReSharper disable once ForCanBeConvertedToForeach
            // ReSharper disable once LoopCanBeConvertedToQuery
            for (var i = 0; i < lexers.Length; i++)
            {
                if (lexers[i] == null)
                {
                    throw new ArgumentException("Precondition: lexers.All(lexer => lexer != null", "lexers");
                }
            }

            this.lexers = lexers;
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, Element previousElementOrNull, out Alternative element)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException("scanner");
            }

            // ReSharper disable once ForCanBeConvertedToForeach
            for (var i = 0; i < this.lexers.Length; i++)
            {
                Element alternative;
                if (this.lexers[i].TryReadElement(scanner, null, out alternative))
                {
                    element = new Alternative(alternative, i + 1);
                    if (previousElementOrNull != null)
                    {
                        element.PreviousElement = previousElementOrNull;
                        previousElementOrNull.NextElement = element;
                    }

                    return true;
                }
            }

            element = default(Alternative);
            return false;
        }
    }
}