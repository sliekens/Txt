namespace TextFx
{
    /// <summary>
    ///     Represents a terminal specification, sometimes called a character.
    /// </summary>
    public class Terminal : Element
    {
        private readonly string value;

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
        public Terminal(string value, ITextContext context)
            : base(value, context)
        {
            this.value = value;
        }

        /// <inheritdoc />
        public override string GetWellFormedText()
        {
            return this.value;
        }
    }
}