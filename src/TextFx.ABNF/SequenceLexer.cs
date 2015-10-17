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
                var lexer = lexers[i];
                if (lexer == null)
                {
                    throw new ArgumentException("Precondition: lexers.All(lexer => lexer != null", nameof(lexers));
                }
            }

            this.lexers = lexers;
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, Element previousElementOrNull, out Sequence element)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }

            var context = scanner.GetContext();
            IList<Element> elements = new List<Element>(this.lexers.Count);
            Element lastResult = null;

            // ReSharper disable once ForCanBeConvertedToForeach
            for (var i = 0; i < this.lexers.Count; i++)
            {
                Element result;
                if (this.lexers[i].TryReadElement(scanner, lastResult, out result))
                {
                    elements.Add(result);
                    lastResult = result;
                }
                else
                {
                    if (elements.Count != 0)
                    {
                        for (var j = elements.Count - 1; j >= 0; j--)
                        {
                            scanner.Unread(elements[j].Text);
                        }
                    }

                    element = default(Sequence);
                    return false;
                }
            }

            element = new Sequence(elements, context);
            if (previousElementOrNull != null)
            {
                element.PreviousElement = previousElementOrNull;
                previousElementOrNull.NextElement = element;
            }

            return true;
        }
    }
}