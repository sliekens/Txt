namespace SLANG
{
    using System;
    using System.Collections.Generic;

    /// <summary>Provides the base class for lexers whose lexer rule is a repetition of elements.</summary>
    /// <typeparam name="TRepetition">The type of the lexer rule.</typeparam>
    /// <typeparam name="TElement">The type of the repeating element.</typeparam>
    public abstract class RepetitionLexer<TRepetition, TElement> : Lexer<TRepetition>
        where TRepetition : Repetition<TElement>
        where TElement : Element
    {
        private readonly int lowerBound;
        private readonly int upperBound;

        /// <summary>Initializes a new instance of the <see cref="RepetitionLexer{TRepetition,TElement}"/> class for a specified rule.
        /// </summary>
        /// <param name="ruleName">The name of the lexer rule. Rule names are case insensitive.</param>
        /// <exception cref="ArgumentException">The value of <paramref name="ruleName"/> is a <c>null</c> reference (<c>Nothing</c> in Visual Basic) -or- the value of <paramref name="ruleName"/> does not start with a letter -or- the value of <paramref name="ruleName"/> contains one or more characters that are not letters, digits or hyphens.</exception>
        /// <param name="lowerBound">A number that indicates the minimum number of occurrences.</param>
        /// <param name="upperBound">A number that indicates the maximum number of occurrences.</param>
        protected RepetitionLexer(string ruleName, int lowerBound, int upperBound)
            : base(ruleName)
        {
            this.lowerBound = lowerBound;
            this.upperBound = upperBound;
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, out TRepetition element)
        {
            if (scanner.EndOfInput && this.lowerBound != 0)
            {
                element = default(TRepetition);
                return false;
            }

            var context = scanner.GetContext();
            var elements = new List<TElement>(this.lowerBound);
            for (int i = 0; i < this.upperBound; i++)
            {
                TElement entry;
                if (this.TryRead(scanner, this.lowerBound, this.upperBound, i, out entry))
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

                element = default(TRepetition);
                return false;
            }

            element = this.CreateInstance(elements, this.lowerBound, this.upperBound, context);
            if (element == null)
            {
                throw new InvalidOperationException("Postcondition: result != null");
            }

            return true;
        }

        /// <summary>Creates a new instance of the lexer rule with the given elements.</summary>
        /// <param name="elements">Every occurrence of the element.</param>
        /// <param name="lowerBound">A number that indicates the minimum number of occurrences.</param>
        /// <param name="upperBound">A number that indicates the maximum number of occurrences.</param>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        /// <returns>An instance of the lexer rule.</returns>
        protected abstract TRepetition CreateInstance(IList<TElement> elements, int lowerBound, int upperBound, ITextContext context);

        /// <summary>Attempts to read an occurrence of the repeating element. A return value indicates whether the element was available.</summary>
        /// <param name="scanner">The scanner object that provides text symbols as well as contextual information about the text source.</param>
        /// <param name="lowerBound">A number that indicates the minimum number of occurrences.</param>
        /// <param name="upperBound">A number that indicates the maximum number of occurrences.</param>
        /// <param name="current">A number that indicates the current number of occurrences.</param>
        /// <param name="element">When this method returns, contains the next available element, or a <c>null</c> reference, depending on whether the return value indicates success.</param>
        /// <returns><c>true</c> to indicate success; otherwise, <c>false</c>.</returns>
        protected abstract bool TryRead(ITextScanner scanner, int lowerBound, int upperBound, int current, out TElement element);
    }
}