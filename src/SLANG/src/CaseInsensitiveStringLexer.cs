namespace SLANG
{
    using System;

    public class CaseInsensitiveStringLexer : Lexer<Element>
    {
        private readonly char[] terminals;

        public CaseInsensitiveStringLexer(char[] terminals)
        {
            if (terminals == null)
            {
                throw new ArgumentNullException("terminals", "Precondition: terminals != null");
            }

            if (terminals.Length == 0)
            {
                throw new ArgumentException("Precondition: terminals.Length > 0", "terminals");
            }

            this.terminals = terminals;
        }

        public override bool TryRead(ITextScanner scanner, out Element element)
        {
            if (scanner.EndOfInput)
            {
                element = default(Element);
                return false;
            }

            var context = scanner.GetContext();
            var buffer = new char[this.terminals.Length];
            for (int i = 0; i < this.terminals.Length; i++)
            {
                if (!scanner.EndOfInput && scanner.TryMatchIgnoreCase(this.terminals[i], out buffer[i]))
                {
                    continue;
                }

                // When the code reaches this block, it means that one of two things
                // * the scanner unexpectedly reached the end of the input
                // * the next terminal is not the expected character
                for (int j = buffer.Length - 1; j >= 0; j--)
                {
                    scanner.PutBack(buffer[j]);
                }

                element = default(Element);
                return false;
            }

            element = new Element(new string(buffer), context);
            return true;
        }
    }
}
