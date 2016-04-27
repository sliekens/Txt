using System;
using System.Diagnostics;
using Jetbrains.Annotations;

namespace Txt.ABNF
{
    /// <summary>
    ///     Represents a terminal specification.
    /// </summary>
    public class Terminal : Element
    {
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly string text;

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
        ///     Creates a new instance of the <see cref="Terminal" /> class with a specified string of terminal values and its
        ///     context.
        /// </summary>
        /// <param name="text">The terminal value.</param>
        /// <param name="context">An object that describes the current element's context.</param>
        public Terminal([NotNull] string text, [NotNull] ITextContext context)
            : base(text, context)
        {
            this.text = text;
        }

        /// <inheritdoc />
        public override string GetWellFormedText()
        {
            return text;
        }
    }
}
