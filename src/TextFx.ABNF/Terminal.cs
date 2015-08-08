namespace TextFx.ABNF
{
    using System;

    /// <summary>
    ///     Represents a terminal specification, sometimes called a character.
    /// </summary>
    public class Terminal : Element
    {
        private readonly char value;

        /// <summary>
        ///     Creates a new instance of the <see cref="Terminal" /> class with a specified terminal to copy.
        /// </summary>
        /// <param name="terminal">The terminal element to copy.</param>
        public Terminal(Terminal terminal)
            : base(terminal)
        {
        }

        /// <summary>
        ///     Creates a new instance of the <see cref="Terminal" /> class with a specified character and context.
        /// </summary>
        /// <param name="value">The terminal value.</param>
        /// <param name="context">An object that describes the current element's context.</param>
        public Terminal(char value, ITextContext context)
            : base(value, context)
        {
            this.value = value;
        }

        public static explicit operator char(Terminal instance)
        {
            return instance.value;
        }

        /// <summary>
        ///     Converts the current instance to its equivalent string representation in a specified base.
        /// </summary>
        /// <param name="toBase">The base of the return value, which must be 2, 8, 10, or 16.</param>
        /// <returns>The converted value in the specified base.</returns>
        /// <exception cref="ArgumentException"><paramref name="toBase" /> is not 2, 8, 10, or 16.</exception>
        public string ToBase(int toBase)
        {
            return Convert.ToString(this.value, toBase).ToUpperInvariant();
        }

        /// <summary>Converts the value of this instance to a value of type <see cref="char" />.</summary>
        /// <returns>The converted value.</returns>
        public char ToChar()
        {
            return this.value;
        }
        /// <inheritdoc />
        public override string GetWellFormedText()
        {
            return this.Text;
        }
    }
}