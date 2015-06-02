namespace SLANG
{
    using System.Collections.Generic;

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
            this.terminals = terminals;
        }

        public IList<Terminal> Terminals
        {
            get
            {
                return this.terminals;
            }
        }
    }
}
