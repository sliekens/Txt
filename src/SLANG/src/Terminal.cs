namespace SLANG
{
    public class Terminal : Element
    {
        public Terminal(Terminal element)
            : base(element)
        {
        }

        public Terminal(char data, ITextContext context)
            : base(data, context)
        {
        }
    }
}