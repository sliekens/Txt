namespace TextFx.ABNF
{
    using System;
    using System.Collections.Generic;

    public class SequenceLexer : Lexer<Sequence>
    {
        private readonly IList<ILexer> lexers;

        public SequenceLexer(params ILexer[] lexers)
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
                var lexer = lexers[i];
                if (lexer == null)
                {
                    throw new ArgumentException("Precondition: lexers.All(lexer => lexer != null", "lexers");
                }
            }

            this.lexers = lexers;
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, out Sequence element)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException("scanner");
            }

            var context = scanner.GetContext();
            IList<Element> elements = new List<Element>(this.lexers.Count);

            // ReSharper disable once ForCanBeConvertedToForeach
            for (var i = 0; i < this.lexers.Count; i++)
            {
                Element terminals;
                if (this.lexers[i].TryReadElement(scanner, out terminals))
                {
                    elements.Add(terminals);
                }
                else
                {
                    if (elements.Count != this.lexers.Count && elements.Count != 0)
                    {
                        for (var j = elements.Count - 1; j >= 0; j--)
                        {
                            scanner.PutBack(elements[j].Value);
                        }
                    }

                    element = default(Sequence);
                    return false;
                }
            }

            element = new Sequence(elements, context);
            return true;
        }
    }
}