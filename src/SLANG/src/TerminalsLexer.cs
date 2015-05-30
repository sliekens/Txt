namespace SLANG
{
    using System;
    using System.Diagnostics;

    /// <summary>Represents a lexer that reads terminal values (sometimes called characters).</summary>
    public class TerminalsLexer : Lexer<Element>
    {
        private readonly char[] terminals;

        public TerminalsLexer(char terminal)
            : this(terminals: terminal)
        {
            this.terminals = new[] { terminal };
        }

        public TerminalsLexer(params char[] terminals)
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

        public char[] Terminals
        {
            get
            {
                Debug.Assert(this.terminals != null, "this.terminals != null");
                return this.terminals;
            }
        }

        public override bool TryRead(ITextScanner scanner, out Element element)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException("scanner", "Precondition: scanner != null");
            }

            var context = scanner.GetContext();
            var values = this.Terminals;
            for (var i = 0; i < values.Length; i++)
            {
                if (!scanner.EndOfInput && scanner.TryMatch(values[i]))
                {
                    continue;
                }

                // When the code reaches this block, it means that one of two things
                // * the scanner unexpectedly reached the end of the input
                // * the next terminal is not the expected character
                if (i != 0)
                {
                    for (var j = i - 1; j >= 0; j--)
                    {
                        scanner.PutBack(values[j].ToString());
                    }
                }

                element = default(Element);
                return false;
            }

            element = new Element(new string(values), context);
            return true;
        }
    }
}
