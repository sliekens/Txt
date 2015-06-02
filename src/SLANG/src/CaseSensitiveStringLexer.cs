namespace SLANG
{
    using System;

    public class CaseSensitiveStringLexer : Lexer<Element>
    {
        private readonly char[] terminals;

        public CaseSensitiveStringLexer(char[] terminals)
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
            if (scanner == null)
            {
                throw new ArgumentNullException("scanner", "Precondition: scanner != null");
            }

            var context = scanner.GetContext();
            for (var i = 0; i < this.terminals.Length; i++)
            {
                if (!scanner.EndOfInput && scanner.TryMatch(this.terminals[i]))
                {
                    continue;
                }

                // When the code reaches this block, it means that one of two things
                // * the scanner unexpectedly reached the end of the input
                // * the next terminal is not the expected character
                for (int j = i - 1; j >= 0; j--)
                {
                    scanner.PutBack(this.terminals[j]);
                }

                element = default(Element);
                return false;
            }

            element = new Element(new string(this.terminals), context);
            return true;
        }
    }
}
