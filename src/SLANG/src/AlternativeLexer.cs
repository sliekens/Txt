namespace SLANG
{
    using System.Diagnostics;

    /// <summary>Provides the base class for lexers whose lexer rule has a range of alternatives.</summary>
    /// <typeparam name="TAlternative">The type of the lexer rule.</typeparam>
    public abstract class AlternativeLexer<TAlternative> : Lexer<TAlternative>
        where TAlternative : Alternative
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly char lowerBound;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly char upperBound;

        /// <summary>Initializes a new instance of the <see cref="AlternativeLexer{TAlternative}"/> class for an unnamed element.</summary>
        /// <param name="lowerBound">The lower bound of the range of alternatives.</param>
        /// <param name="upperBound">The upper bound of the range of alternatives.</param>
        protected AlternativeLexer(char lowerBound, char upperBound)
        {
            this.lowerBound = lowerBound;
            this.upperBound = upperBound;
        }

        /// <summary>Initializes a new instance of the <see cref="AlternativeLexer{TAlternative}"/> class for a specified rule.</summary>
        /// <param name="ruleName">The name of the lexer rule. Rule names are case insensitive.</param>
        /// <param name="lowerBound">The lower bound of the range of alternatives.</param>
        /// <param name="upperBound">The upper bound of the range of alternatives.</param>
        protected AlternativeLexer(string ruleName, char lowerBound, char upperBound)
            : base(ruleName)
        {
            this.lowerBound = lowerBound;
            this.upperBound = upperBound;
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, out TAlternative element)
        {
            if (scanner.EndOfInput)
            {
                element = default(TAlternative);
                return false;
            }

            var context = scanner.GetContext();
            for (char c = this.lowerBound; c <= this.upperBound; c++)
            {
                if (scanner.TryMatch(c, ignoreCase: false))
                {
                    element = this.CreateInstance(c, context);
                    return true;
                }
            }

            element = default(TAlternative);
            return false;
        }

        /// <summary>Creates a new instance of the lexer rule for a given alternative element.</summary>
        /// <param name="element">The alternative element.</param>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        /// <returns>An instance of the lexer rule.</returns>
        protected abstract TAlternative CreateInstance(char element, ITextContext context);
    }
}
