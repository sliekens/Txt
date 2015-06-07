namespace SLANG
{
    using System;
    using System.Collections.Generic;

    public class AlternativeLexer : Lexer<Alternative>
    {
        private readonly IList<ILexer> lexers;

        public AlternativeLexer(params ILexer[] lexers)
        {
            if (lexers == null)
            {
                throw new ArgumentNullException("lexers", "Precondition: lexers != null");
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
                throw new ArgumentNullException("scanner", "Precondition: scanner != null");
            }

            // ReSharper disable once ForCanBeConvertedToForeach
            for (var i = 0; i < this.lexers.Count; i++)
            {
                Element alternative;
                if (this.lexers[i].TryReadElement(scanner, out alternative))
                {
                    element = new Alternative(alternative);
                    return true;
                }
            }

            element = default(Alternative);
            return false;
        }
    }
}