namespace SLANG
{
    using System;
    using System.Collections.Generic;

    public abstract class RepetitionLexer<T, T2> : Lexer<T>
        where T : Repetition<T2>
        where T2 : Element
    {
        private readonly int lowerBound;
        private readonly int upperBound;

        public RepetitionLexer(string ruleName, int lowerBound, int upperBound)
            : base(ruleName)
        {
            this.lowerBound = lowerBound;
            this.upperBound = upperBound;
        }

        public override bool TryRead(ITextScanner scanner, out T element)
        {
            if (scanner.EndOfInput && this.lowerBound != 0)
            {
                element = default(T);
                return false;
            }

            var context = scanner.GetContext();
            var elements = new List<T2>(this.lowerBound);
            for (int i = 0; i < this.upperBound; i++)
            {
                T2 entry;
                if (this.TryRead(scanner, out entry))
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

                element = default(T);
                return false;
            }

            element = this.CreateInstance(elements, this.lowerBound, this.upperBound, context);
            if (element == null)
            {
                throw new InvalidOperationException("Postcondition: result != null");
            }

            return true;
        }

        protected abstract T CreateInstance(IList<T2> elements, int lowerBound, int upperBound, ITextContext context);

        protected abstract bool TryRead(ITextScanner scanner, out T2 element);
    }
}