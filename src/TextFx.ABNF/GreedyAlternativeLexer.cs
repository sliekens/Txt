namespace TextFx.ABNF
{
    using System;

    /// <summary>
    ///     Wraps a collection of <see cref="ILexer" /> and tests their <see cref="ILexer.TryReadElement" /> method until the
    ///     longest match is found.
    ///     This class implements a greedy algorithm. For a first-match-wins algorithm, use the
    ///     <see cref="AlternativeLexer" /> class instead.
    /// </summary>
    public class GreedyAlternativeLexer : Lexer<Alternative>
    {
        private readonly ILexer[] lexers;

        public GreedyAlternativeLexer(ILexer[] lexers)
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

        public override bool TryRead(ITextScanner scanner, out Alternative element)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException("scanner");
            }

            ILexer bestChoice = null;
            var bestChoiceLength = -1;
            var ordinal = 0;

            // ReSharper disable once ForCanBeConvertedToForeach
            for (var i = 0; i < this.lexers.Length; i++)
            {
                var lexer = this.lexers[i];
                Element alternative;
                if (lexer.TryReadElement(scanner, out alternative))
                {
                    var length = alternative.Value.Length;
                    if (length > bestChoiceLength)
                    {
                        bestChoice = lexer;
                        bestChoiceLength = length;
                        ordinal = i;
                    }

                    scanner.Unread(alternative.Value);
                }
            }

            Element result;
            if (bestChoice == null || !bestChoice.TryReadElement(scanner, out result))
            {
                element = default(Alternative);
                return false;
            }

            element = new Alternative(result, ordinal);
            return true;
        }
    }
}