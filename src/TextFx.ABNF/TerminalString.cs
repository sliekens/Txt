namespace TextFx.ABNF
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    ///     Represents a string of terminal values.
    /// </summary>
    public class TerminalString : Element
    {
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
            : base(terminals.Cast<Element>().ToList(), context)
        {
        }
    }
}