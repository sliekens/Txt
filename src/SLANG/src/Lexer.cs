// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Lexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   Provides the base class for lexers. A lexer is a class that matches symbols from a data source against a grammar rule to produce grammar elements. Each class that extends the <see cref="Lexer{TElement}" /> class corresponds to a singe grammar rule. For complex grammars with many grammar rules, multiple lexers work together to convert the input text to a parse tree.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG
{
    using System;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using System.Text;

    /// <summary>Provides the base class for lexers. A lexer is a class that matches symbols from a data source against a grammar rule to produce grammar elements. Each class that extends the <see cref="Lexer{TElement}"/> class corresponds to a singe grammar rule. For complex grammars with many grammar rules, multiple lexers work together to convert the input text to a parse tree.</summary>
    /// <typeparam name="TElement">The type of the element that represents the lexer rule.</typeparam>
    /// <remarks><para>The terms "lexer rule" and "grammar rule" are used interchangeably.</para>
    /// <para>Notes to inheritors.
    /// The name of grammar rules are case insensitive.
    /// At minimum, you must provide an implementation for the <see cref="TryRead"/> method. You can optionally provide a custom implementation for the <see cref="Read"/> method. The default behavior of the <see cref="Read"/> method is essentially a virtual call to <see cref="TryRead"/>, but contains additional logic to initialize and throw a <see cref="SyntaxErrorException"/>.
    /// There are a number of conventions that you should follow.
    /// If the value of <see cref="ITextScanner.EndOfInput"/> is <c>true</c> and the grammar rule is not optional, you should immediately return <c>false</c>.
    /// Do not throw any exceptions in TryRead().
    /// Lexer classes should be sealed.
    /// Re-use lexer classes for lexer rules that reference other lexer rules. 
    /// <example>INT = 1*DIGIT
    /// <code>
    /// public sealed class DigitLexer : Lexer&lt;Digit&gt;
    /// {
    ///     public DigitLexer() : base("DIGIT") { }
    /// 
    ///     public override bool TryRead(ITextScanner scanner, out Digit element)
    ///     {
    ///         // Actual implementation
    ///     }
    /// }
    /// 
    /// public sealed class IntLexer : Lexer&lt;Int&gt;
    /// {
    ///     private readonly ILexer&lt;Digit&gt; digitLexer;
    /// 
    ///     public IntLexer() : this(new DigitLexer()) { }
    /// 
    ///     public IntLexer(ILexer&lt;Digit&gt; digitLexer)
    ///         : base("INT")
    ///     {
    ///         this.digitLexer = digitLexer
    ///     }
    /// 
    ///     public override bool TryRead(ITextScanner scanner, out Int element)
    ///     {
    ///         // Actual implementation
    ///     }
    /// }</code>
    /// </example>
    /// </para>
    /// </remarks>
    public abstract class Lexer<TElement> : ILexer<TElement>
        where TElement : Element
    {
        /// <summary>The name of the lexer rule.</summary>
        private readonly string ruleName;

        /// <summary>Initializes a new instance of the <see cref="Lexer{TElement}"/> class for a specified rule.</summary>
        /// <param name="ruleName">The name of the lexer rule. Rule names are case insensitive.</param>
        /// <exception cref="ArgumentException">The value of <paramref name="ruleName"/> is a <c>null</c> reference (<c>Nothing</c> in Visual Basic) -or- the value of <paramref name="ruleName"/> does not start with a letter -or- the value of <paramref name="ruleName"/> contains one or more characters that are not letters, digits or hyphens.</exception>
        protected Lexer(string ruleName)
        {
            if (string.IsNullOrEmpty(ruleName))
            {
                throw new ArgumentException("Precondition failed: !string.IsNullOrEmpty(ruleName)", "ruleName");
            }

            if (!char.IsLetter(ruleName, 0))
            {
                throw new ArgumentException("Precondition failed: char.IsLetter(ruleName, 0)");
            }

            if (ruleName.ToCharArray().Any(c => !char.IsLetterOrDigit(c) && c != '-'))
            {
                throw new ArgumentException(
                    "Precondition failed: ruleName.ToCharArray().All(c => char.IsLetterOrDigit(c) || c == '-')");
            }

            this.ruleName = ruleName;
        }

        /// <inheritdoc />
        public string RuleName
        {
            get
            {
                return this.ruleName;
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

            throw new SyntaxErrorException(
                scanner.GetContext(),
                string.Format("Unexpected symbol. Expected element: '{0}'.", this.ruleName));
        }

        /// <inheritdoc />
        public abstract bool TryRead(ITextScanner scanner, out TElement element);

        /// <summary>Utility method. Reads the next specified character. The comparison is case-sensitive. A return value indicates whether the character was available.</summary>
        /// <param name="scanner"></param>
        /// <param name="c">The character to read.</param>
        /// <param name="element">When this method returns, contains the next available element, or a <c>null</c> reference, depending
        /// on whether the return value indicates success.</param>
        /// <returns><c>true</c> to indicate success; otherwise, <c>false</c>.</returns>
        protected static bool TryReadTerminal(ITextScanner scanner, char c, out Element element)
        {
            Contract.Requires(scanner != null);
            if (scanner.EndOfInput)
            {
                element = default(Element);
                return false;
            }

            var context = scanner.GetContext();
            if (scanner.TryMatch(c))
            {
                element = new Element(c, context);
                return true;
            }

            element = default(Element);
            return false;
        }

        /// <summary>Utility method. Reads the next specified terminal. The comparison is case-insensitive. A return value indicates whether the terminal was available.</summary>
        /// <param name="scanner"></param>
        /// <param name="s">The characters to read.</param>
        /// <param name="element">When this method returns, contains the next available element, or a <c>null</c> reference, depending
        /// on whether the return value indicates success.</param>
        /// <returns><c>true</c> to indicate success; otherwise, <c>false</c>.</returns>
        protected static bool TryReadTerminal(ITextScanner scanner, string s, out Element element)
        {
            Contract.Requires(scanner != null);
            Contract.Requires(s != null);
            if (scanner.EndOfInput)
            {
                element = default(Element);
                return false;
            }

            var context = scanner.GetContext();
            var buffer = new StringBuilder(capacity: s.Length);
            for (int i = 0; i < s.Length; i++)
            {
                var next = s[i];
                var actual = scanner.NextCharacter.GetValueOrDefault();
                if (!scanner.EndOfInput && scanner.TryMatch(next, ignoreCase: true))
                {
                    buffer.Append(actual);
                }
                else
                {
                    if (i != 0)
                    {
                        scanner.PutBack(buffer.ToString());
                    }

                    element = default(Element);
                    return false;
                }
            }

            element = new Element(buffer.ToString(), context);
            return true;
        }

        /// <summary>Utility method. Sets a specified element to its default value, and returns <c>false</c>.</summary>
        /// <param name="element">The element to set to its default value.</param>
        /// <returns>Returns <c>false</c>.</returns>
        protected static bool Default(out TElement element)
        {
            element = default(TElement);
            return false;
        }
    }
}