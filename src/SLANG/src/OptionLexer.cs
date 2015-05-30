namespace SLANG
{
    using System;

    /// <summary>Provides the base class for lexers whose lexer rule is an optional element.</summary>
    /// <typeparam name="TOption">The type of the lexer rule.</typeparam>
    /// <typeparam name="TElement">The type of the optional element.</typeparam>
    public abstract class OptionLexer<TOption, TElement> : RepetitionLexer<TOption, TElement>
        where TOption : Option<TElement>
        where TElement : Element
    {
        /// <summary>Initializes a new instance of the <see cref="OptionLexer{TOption,TElement}"/> class for an unnamed element.</summary>
        protected OptionLexer()
            : base(0, 1)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="OptionLexer{TOption,TElement}"/> class for a specified rule.</summary>
        /// <param name="ruleName">The name of the lexer rule. Rule names are case insensitive.</param>
        /// <exception cref="ArgumentException">The value of <paramref name="ruleName"/> is a <c>null</c> reference (<c>Nothing</c> in Visual Basic) -or- the value of <paramref name="ruleName"/> does not start with a letter -or- the value of <paramref name="ruleName"/> contains one or more characters that are not letters, digits or hyphens.</exception>
        protected OptionLexer(string ruleName)
            : base(ruleName, 0, 1)
        {
        }
    }
}
