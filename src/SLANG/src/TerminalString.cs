namespace SLANG
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    public class TerminalString : Element
    {
        private readonly IList<Terminal> terminals;

        public TerminalString(TerminalString element)
            : base(element)
        {
        }

        public TerminalString(IList<Terminal> terminals, ITextContext context)
            : base(string.Concat(terminals), context)
        {
            if (terminals == null)
            {
                throw new ArgumentNullException("terminals", "Precondition: terminals != null");
            }

            this.terminals = terminals;
        }

        public IList<Terminal> Terminals
        {
            get
            {
                Debug.Assert(this.terminals != null, "this.terminals != null");
                return this.terminals;
            }
        }
    }
}
