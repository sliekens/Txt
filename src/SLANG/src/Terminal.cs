namespace SLANG
{
    public class Terminal : Element
    {
        private readonly char terminal;

        public Terminal(Terminal element)
            : base(element)
        {
        }

        public Terminal(char data, ITextContext context)
            : base(data, context)
        {
            this.terminal = data;
        }

        public static explicit operator char(Terminal instance)
        {
            return instance.terminal;
        }

        public char ToChar()
        {
            return this.terminal;
        }
    }
}