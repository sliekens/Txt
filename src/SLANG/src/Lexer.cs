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
    /// </para>
    /// </remarks>
    public abstract class Lexer<TElement> : ILexer<TElement>
        where TElement : Element
    {
        /// <summary>Initializes a new instance of the <see cref="Lexer{TElement}"/> class for an unnamed element.</summary>
        protected Lexer()
        {
        }

        /// <inheritdoc />
        public bool TryReadElement(ITextScanner scanner, out Element element)
        {
            // This intermediary variable is required to match the type of the output parameter
            TElement t;
            if (this.TryRead(scanner, out t))
            {
                element = t;
                return true;
            }

            element = default(Element);
            return false;
        }

        /// <inheritdoc />
        public Element ReadElement(ITextScanner scanner)
        {
            return this.Read(scanner);
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
                string.Format("Unexpected symbol. Expected element: '{0}'.", typeof(TElement).FullName));
        }

        /// <inheritdoc />
        public abstract bool TryRead(ITextScanner scanner, out TElement element);

        /// <summary>Utility method. Reads the next specified character. The comparison is case-sensitive. A return value indicates whether the character was available.</summary>
        /// <param name="scanner"></param>
        /// <param name="c">The character to read.</param>
        /// <param name="element">When this method returns, contains the next available element, or a <c>null</c> reference, depending
        /// on whether the return value indicates success.</param>
        /// <returns><c>true</c> to indicate success; otherwise, <c>false</c>.</returns>
        [Obsolete("Obsolete: use class TerminalsLexer instead")]
        protected static bool TryReadTerminal(ITextScanner scanner, char c, out Element element)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException("scanner", "Precondition: scanner != null");
            }

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

        /// <summary>Utility method. Reads the next specified terminal. The comparison is case-sensitive. A return value indicates whether the terminal was available.</summary>
        /// <param name="scanner"></param>
        /// <param name="s">The characters to read.</param>
        /// <param name="element">When this method returns, contains the next available element, or a <c>null</c> reference, depending
        /// on whether the return value indicates success.</param>
        /// <returns><c>true</c> to indicate success; otherwise, <c>false</c>.</returns>
        [Obsolete("Obsolete: use class TerminalsLexer instead")]
        protected static bool TryReadTerminal(ITextScanner scanner, char[] s, out Element element)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException("scanner", "Precondition: scanner != null");
            }

            if (s == null)
            {
                throw new ArgumentNullException("s", "Precondition: s != null");
            }

            if (scanner.EndOfInput)
            {
                element = default(Element);
                return false;
            }

            var context = scanner.GetContext();
            if (s.Length == 0)
            {
                element = new Element(string.Empty, context);
                return true;
            }

            for (int i = 0; i < s.Length; i++)
            {
                if (scanner.EndOfInput || !scanner.TryMatch(s[i]))
                {
                    if (i != 0)
                    {
                        for (int j = i - 1; j >= 0; j--)
                        {
                            scanner.PutBack(s[j].ToString());
                        }
                    }

                    element = default(Element);
                    return false;
                }
            }

            element = new Element(new string(s), context);
            return true;
        }

        /// <summary>Utility method. Reads the next specified terminal. The comparison is case-insensitive. A return value indicates whether the terminal was available.</summary>
        /// <param name="scanner"></param>
        /// <param name="s">The characters to read.</param>
        /// <param name="element">When this method returns, contains the next available element, or a <c>null</c> reference, depending
        /// on whether the return value indicates success.</param>
        /// <returns><c>true</c> to indicate success; otherwise, <c>false</c>.</returns>
        [Obsolete("Obsolete: use class StringLexer instead")]
        protected static bool TryReadTerminal(ITextScanner scanner, string s, out Element element)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException("scanner", "Precondition: scanner != null");
            }

            if (s == null)
            {
                throw new ArgumentNullException("s", "Precondition: s != null");
            }

            if (scanner.EndOfInput)
            {
                element = default(Element);
                return false;
            }

            var context = scanner.GetContext();
            var buffer = new char[s.Length];
            for (int i = 0; i < s.Length; i++)
            {
                var c = s[i];
                if (!scanner.EndOfInput && scanner.TryMatchIgnoreCase(c, out c))
                {
                    buffer[i] = c;
                }
                else
                {
                    if (i != 0)
                    {
                        for (int j = i - 1; j >= 0; j--)
                        {
                            scanner.PutBack(buffer[j]);
                        }
                    }

                    element = default(Element);
                    return false;
                }
            }

            element = new Element(new string(buffer), context);
            return true;
        }
    }
}