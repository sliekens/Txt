namespace SLANG
{
    using System.Collections.Generic;

    public abstract class RepetitionLexer<T> : Lexer<Repetition<T>>
        where T : Element
    {
        private readonly int lowerBound;
        private readonly int upperBound;

        public RepetitionLexer(string ruleName, int lowerBound, int upperBound)
            : base(ruleName)
        {
            this.lowerBound = lowerBound;
            this.upperBound = upperBound;
        }

        public override bool TryRead(ITextScanner scanner, out Repetition<T> element)
        {
            if (scanner.EndOfInput && this.lowerBound != 0)
            {
                element = default(Repetition<T>);
                return false;
            }

            var context = scanner.GetContext();
            var elements = new List<T>(this.upperBound);
            for (int i = 0; i < this.upperBound; i++)
            {
                T entry;
                if (this.TryReadOne(scanner, out entry))
                {
                    elements.Add(entry);
                }
                else
                {
                    break;
                }
            }

            if (elements.Count < this.lowerBound)
            {
                if (elements.Count != 0)
                {
                    for (int i = elements.Count - 1; i >= 0; i--)
                    {
                        scanner.PutBack(elements[i].Data);
                    }
                }

                element = default(Repetition<T>);
                return false;
            }

            element = new Repetition<T>(elements, this.lowerBound, this.upperBound, context);
            return true;
        }

        protected abstract bool TryReadOne(ITextScanner scanner, out T element);
    }
}