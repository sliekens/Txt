namespace SLANG
{
    /// <summary>Represents a lexer that reads terminal values (sometimes called characters) using case-insensitive comparisons.</summary>
    public class StringLexer : TerminalsLexer
    {
        public StringLexer(string terminals)
            : base(terminals.ToCharArray())
        {
        }

        public override bool TryRead(ITextScanner scanner, out Element element)
        {
            if (scanner.EndOfInput)
            {
                element = default(Element);
                return false;
            }

            var context = scanner.GetContext();
            var values = this.Terminals;
            for (int i = 0; i < values.Length; i++)
            {
                if (!scanner.EndOfInput && scanner.TryMatchIgnoreCase(values[i], out values[i]))
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
                        scanner.PutBack(values[j]);
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
