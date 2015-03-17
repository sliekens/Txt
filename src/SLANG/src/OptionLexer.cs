namespace SLANG
{
    /// <summary>Provides the base class for lexers whose lexer rule is an optional element.</summary>
    /// <typeparam name="TOption">The type of the lexer rule.</typeparam>
    /// <typeparam name="TElement">The type of the optional element.</typeparam>
    public abstract class OptionLexer<TOption, TElement> : RepetitionLexer<TOption, TElement>
        where TOption : Option<TElement>
        where TElement : Element
    {
        /// <summary>Initializes a new instance of the <see cref="OptionLexer{TOption,TElement}"/> class for a specified rule.</summary>
        /// <param name="ruleName">The name of the lexer rule. Rule names are case insensitive.</param>
        protected OptionLexer(string ruleName)
            : base(ruleName, 0, 1)
        {
        }
    }
}
