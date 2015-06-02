namespace SLANG
{
    using System;
    using System.Collections.Generic;

    public class StringLexer : Lexer<TerminalString>
    {
        private readonly IList<ILexer<Terminal>> lexers; 

        public StringLexer(IList<ILexer<Terminal>> lexers)
        {
            if (lexers == null)
            {
                throw new ArgumentNullException("lexers", "Precondition: lexers != null");
            }

            this.lexers = lexers;
        }

        public override bool TryRead(ITextScanner scanner, out TerminalString element)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException("scanner", "Precondition: scanner != null");
            }

            var context = scanner.GetContext();
            IList<Terminal> elements = new List<Terminal>(this.lexers.Count);

            // ReSharper disable once ForCanBeConvertedToForeach
            for (var i = 0; i < this.lexers.Count; i++)
            {
                Terminal terminals;
                if (this.lexers[i].TryRead(scanner, out terminals))
                {
                    elements.Add(terminals);
                }
                else
                {
                    if (elements.Count != this.lexers.Count && elements.Count != 0)
                    {
                        for (int j = elements.Count - 1; j >= 0; j--)
                        {
                            scanner.PutBack(elements[j].Data);
                        }
                    }

                    element = default(TerminalString);
                    return false;
                }
            }

            element = new TerminalString(elements, context);
            return true;
        }
    }
}
