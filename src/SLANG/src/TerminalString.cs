namespace SLANG
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    /// <summary>
    ///     Represents a string of terminal values.
    /// </summary>
    public class TerminalString : Element
    {
        private readonly IList<Terminal> terminals;

        /// <summary>
        ///     Initializes a new instance of the <see cref="TerminalString" /> class with a specified terminal string to copy.
        /// </summary>
        /// <param name="element">The terminal string to copy.</param>
        public TerminalString(TerminalString element)
            : base(element)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="TerminalString" /> class with the given terminal values and context.
        /// </summary>
        /// <param name="terminals">The terminal values, in order of appearance.</param>
        /// <param name="context">An object that describes the current element's context.</param>
        public TerminalString(IList<Terminal> terminals, ITextContext context)
            : base(string.Concat(terminals), context)
        {
            if (terminals == null)
            {
                throw new ArgumentNullException("terminals", "Precondition: terminals != null");
            }

            this.terminals = terminals;
        }

        /// <summary>
        ///     Gets the terminal values in the string, in order of appearance.
        /// </summary>
        public IList<Terminal> Terminals
        {
            get
            {
                Debug.Assert(this.terminals != null, "this.terminals != null");
                return this.terminals;
            }
        }

        /// <summary>
        ///     Converts the current instance to its equivalent string representation in a specified base.
        /// </summary>
        /// <param name="toBase">The base of the return value, which must be 2, 10 or 16.</param>
        /// <returns>The converted value in a specified base.</returns>
        /// <exception cref="ArgumentException"><paramref name="toBase" /> is not 2, 10, or 16.</exception>
        public string ToBase(int toBase)
        {
            var values = this.Terminals.Select(terminal => terminal.ToBase(toBase));
            var valuesAsString = string.Join(".", values);
            switch (toBase)
            {
                case 2:
                    return "%b" + valuesAsString;
                case 10:
                    return "%d" + valuesAsString;
                case 16:
                    return "%x" + valuesAsString;
                default:
                    throw new ArgumentException("The given base is not currently supported.", "toBase");
            }
        }
    }
}