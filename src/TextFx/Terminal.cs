namespace TextFx
{
    using System;
    using System.Diagnostics;
    using JetBrains.Annotations;

    /// <summary>
    ///     Represents a terminal specification.
    /// </summary>
    public class Terminal : Element
    {
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly string value;

        /// <summary>
        ///     Creates a new instance of the <see cref="Terminal" /> class with a specified terminal to copy.
        /// </summary>
        /// <param name="terminal">The terminal element to copy.</param>
        /// <exception cref="ArgumentNullException">The value of <paramref name="terminal" /> is a null reference.</exception>
        public Terminal([NotNull] Terminal terminal)
            : base(terminal)
        {
        }

        /// <summary>
        ///     Creates a new instance of the <see cref="Terminal" /> class with a specified string and context.
        /// </summary>
        /// <param name="value">The terminal value.</param>
        /// <param name="context">An object that describes the current element's context.</param>
        public Terminal(char value, [NotNull] ITextContext context)
            : this(char.ToString(value), context)
        {
        }

        /// <summary>
        ///     Creates a new instance of the <see cref="Terminal" /> class with a specified character and context.
        /// </summary>
        /// <param name="value">The terminal value.</param>
        /// <param name="context">An object that describes the current element's context.</param>
        public Terminal([NotNull] string value, [NotNull] ITextContext context)
            : base(value, context)
        {
            this.value = value;
        }

        /// <inheritdoc />
        public override string GetWellFormedText()
        {
            return value;
        }
    }
}
