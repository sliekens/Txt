namespace SLANG
{
    using System.Diagnostics;

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

        public char ToChar()
        {
            Debug.Assert(this.Data.Length == 1, "this.Data.Length == 1");
            return this.Data[0];
        }
    }
}