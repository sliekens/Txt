namespace SLANG
{
    /// <summary>
    ///     Represents a terminal specification, sometimes called a character.
    /// </summary>
    public class Terminal : Element
    {
        private readonly char terminal;

        /// <summary>
        ///     Creates a new instance of the <see cref="Terminal" /> class with a specified terminal to copy.
        /// </summary>
        /// <param name="element">The terminal element to copy.</param>
        public Terminal(Terminal element)
            : base(element)
        {
        }

        /// <summary>
        ///     Creates a new instance of the <see cref="Terminal" /> class with a specified character and context.
        /// </summary>
        /// <param name="data">The terminal value.</param>
        /// <param name="context">An object that describes the current element's context.</param>
        public Terminal(char data, ITextContext context)
            : base(data, context)
        {
            this.terminal = data;
        }

        public static explicit operator char(Terminal instance)
        {
            return instance.terminal;
        }

        /// <summary>Converts the value of this instance to a value of type <see cref="char" />.</summary>
        /// <returns>The converted value.</returns>
        public char ToChar()
        {
            return this.terminal;
        }
    }
}