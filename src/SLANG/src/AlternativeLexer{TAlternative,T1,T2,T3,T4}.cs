// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AlternativeLexer{TAlternative,T1,T2,T3,T4}.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   Provides the base class for lexers whose lexer rule has four alternatives.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG
{
    using System;

    using Microsoft.Practices.ServiceLocation;

    /// <summary>Provides the base class for lexers whose lexer rule has four alternatives.</summary>
    /// <typeparam name="TAlternative">The type of the lexer rule.</typeparam>
    /// <typeparam name="T1">The type of the first alternative element.</typeparam>
    /// <typeparam name="T2">The type of the second alternative element.</typeparam>
    /// <typeparam name="T3">The type of the third alternative element.</typeparam>
    /// <typeparam name="T4">The type of the fourth alternative element.</typeparam>
    public abstract class AlternativeLexer<TAlternative, T1, T2, T3, T4> : Lexer<TAlternative>
        where TAlternative : Alternative<T1, T2, T3, T4>
        where T1 : Element
        where T2 : Element
        where T3 : Element
        where T4 : Element
    {
        /// <summary>Initializes a new instance of the <see cref="AlternativeLexer{TAlternative,T1,T2,T3,T4}"/> class for an unnamed element.</summary>
        /// <param name="serviceLocator">The object that retrieves instances of <see cref="ILexer{TElement}"/> by type and optional rule name.</param>
        protected AlternativeLexer(IServiceLocator serviceLocator)
            : base(serviceLocator)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="AlternativeLexer{TAlternative,T1,T2,T3,T4}"/> class for a specified rule.</summary>
        /// <param name="serviceLocator">The object that retrieves instances of <see cref="ILexer{TElement}"/> by type and optional rule name.</param>
        /// <param name="ruleName">The name of the lexer rule. Rule names are case insensitive.</param>
        /// <exception cref="ArgumentException">The value of <paramref name="ruleName"/> is a <c>null</c> reference (<c>Nothing</c> in Visual Basic) -or- the value of <paramref name="ruleName"/> does not start with a letter -or- the value of <paramref name="ruleName"/> contains one or more characters that are not letters, digits or hyphens.</exception>
        protected AlternativeLexer(IServiceLocator serviceLocator, string ruleName)
            : base(serviceLocator, ruleName)
        {
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, out TAlternative element)
        {
            if (scanner.EndOfInput)
            {
                element = default(TAlternative);
                return false;
            }

            T1 alternative1;
            if (this.TryRead1(scanner, out alternative1))
            {
                element = this.CreateInstance1(alternative1);
                return true;
            }

            T2 alternative2;
            if (this.TryRead2(scanner, out alternative2))
            {
                element = this.CreateInstance2(alternative2);
                return true;
            }

            T3 alternative3;
            if (this.TryRead3(scanner, out alternative3))
            {
                element = this.CreateInstance3(alternative3);
                return true;
            }

            T4 alternative4;
            if (this.TryRead4(scanner, out alternative4))
            {
                element = this.CreateInstance4(alternative4);
                return true;
            }

            element = default(TAlternative);
            return false;
        }

        /// <summary>Creates a new instance of the lexer rule for the first alternative element.</summary>
        /// <param name="element">The alternative element.</param>
        /// <returns>An instance of the lexer rule.</returns>
        protected virtual TAlternative CreateInstance1(T1 element)
        {
            return (TAlternative)Activator.CreateInstance(typeof(TAlternative), element);
        }

        /// <summary>Creates a new instance of the lexer rule for the second alternative element.</summary>
        /// <param name="element">The alternative element.</param>
        /// <returns>An instance of the lexer rule.</returns>
        protected virtual TAlternative CreateInstance2(T2 element)
        {
            return (TAlternative)Activator.CreateInstance(typeof(TAlternative), element);
        }

        /// <summary>Creates a new instance of the lexer rule for the third alternative element.</summary>
        /// <param name="element">The alternative element.</param>
        /// <returns>An instance of the lexer rule.</returns>
        protected virtual TAlternative CreateInstance3(T3 element)
        {
            return (TAlternative)Activator.CreateInstance(typeof(TAlternative), element);
        }

        /// <summary>Creates a new instance of the lexer rule for the fourth alternative element.</summary>
        /// <param name="element">The alternative element.</param>
        /// <returns>An instance of the lexer rule.</returns>
        protected virtual TAlternative CreateInstance4(T4 element)
        {
            return (TAlternative)Activator.CreateInstance(typeof(TAlternative), element);
        }

        /// <summary>Attempts to read the first alternative element. A return value indicates whether the element was available.</summary>
        /// <param name="scanner">The scanner object that provides text symbols as well as contextual information about the text source.</param>
        /// <param name="element">When this method returns, contains the next available element, or a <c>null</c> reference, depending on whether the return value indicates success.</param>
        /// <exception cref="T:System.InvalidOperationException">The given scanner object is not initialized.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The given text scanner is closed.</exception>
        /// <returns><c>true</c> to indicate success; otherwise, <c>false</c>.</returns>
        protected virtual bool TryRead1(ITextScanner scanner, out T1 element)
        {
            return this.Services.GetInstance<ILexer<T1>>().TryRead(scanner, out element);
        }

        /// <summary>Attempts to read the second alternative element. A return value indicates whether the element was available.</summary>
        /// <param name="scanner">The scanner object that provides text symbols as well as contextual information about the text source.</param>
        /// <param name="element">When this method returns, contains the next available element, or a <c>null</c> reference, depending on whether the return value indicates success.</param>
        /// <exception cref="T:System.InvalidOperationException">The given scanner object is not initialized.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The given text scanner is closed.</exception>
        /// <returns><c>true</c> to indicate success; otherwise, <c>false</c>.</returns>
        protected virtual bool TryRead2(ITextScanner scanner, out T2 element)
        {
            return this.Services.GetInstance<ILexer<T2>>().TryRead(scanner, out element);
        }

        /// <summary>Attempts to read the third alternative element. A return value indicates whether the element was available.</summary>
        /// <param name="scanner">The scanner object that provides text symbols as well as contextual information about the text source.</param>
        /// <param name="element">When this method returns, contains the next available element, or a <c>null</c> reference, depending on whether the return value indicates success.</param>
        /// <exception cref="T:System.InvalidOperationException">The given scanner object is not initialized.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The given text scanner is closed.</exception>
        /// <returns><c>true</c> to indicate success; otherwise, <c>false</c>.</returns>
        protected virtual bool TryRead3(ITextScanner scanner, out T3 element)
        {
            return this.Services.GetInstance<ILexer<T3>>().TryRead(scanner, out element);
        }

        /// <summary>Attempts to read the fourth alternative element. A return value indicates whether the element was available.</summary>
        /// <param name="scanner">The scanner object that provides text symbols as well as contextual information about the text source.</param>
        /// <param name="element">When this method returns, contains the next available element, or a <c>null</c> reference, depending on whether the return value indicates success.</param>
        /// <exception cref="T:System.InvalidOperationException">The given scanner object is not initialized.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The given text scanner is closed.</exception>
        /// <returns><c>true</c> to indicate success; otherwise, <c>false</c>.</returns>
        protected virtual bool TryRead4(ITextScanner scanner, out T4 element)
        {
            return this.Services.GetInstance<ILexer<T4>>().TryRead(scanner, out element);
        }
    }
}