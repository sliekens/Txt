// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Lexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Text.Scanning
{
    using System.Diagnostics.Contracts;

    /// <summary>TODO </summary>
    /// <typeparam name="TElement"></typeparam>
    public abstract class Lexer<TElement> : ILexer<TElement>
        where TElement : Element
    {
        private readonly string ruleName;

        protected Lexer(string ruleName)
        {
            Contract.Requires(!string.IsNullOrEmpty(ruleName));
            Contract.Requires(char.IsLetter(ruleName, 0));
            Contract.Ensures(Contract.ForAll(ruleName.ToCharArray(), c => char.IsLetterOrDigit(c) || c == '-'));
            this.ruleName = ruleName;
        }

        public string RuleName
        {
            get
            {
                return this.ruleName;
            }
        }

        /// <inheritdoc />
        public virtual void PutBack(ITextScanner scanner, TElement element)
        {
            var data = element.Data;
            for (var i = data.Length - 1; i >= 0; i--)
            {
                scanner.PutBack(data[i]);
            }
        }

        /// <inheritdoc />
        public virtual TElement Read(ITextScanner scanner)
        {
            TElement element;
            if (this.TryRead(scanner, out element))
            {
                return element;
            }

            throw new SyntaxErrorException(scanner.GetContext(), string.Format("Unexpected symbol. Expected element: '{0}'.", this.ruleName));
        }

        /// <inheritdoc />
        public abstract bool TryRead(ITextScanner scanner, out TElement element);

        /// <summary>Utility method. Sets a specified element to its default value, and returns <c>false</c>.</summary>
        /// <param name="element"></param>
        /// <returns></returns>
        protected static bool Default(out TElement element)
        {
            element = default(TElement);
            return false;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(!string.IsNullOrWhiteSpace(this.ruleName));
        }

    }
}