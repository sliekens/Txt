namespace TextFx.ABNF
{
    using System;
    using System.Collections.Generic;

    /// <summary>Provides methods for reading a string of values.</summary>
    public class StringLexer : Lexer<TerminalString>
    {
        private readonly IList<ILexer<Terminal>> lexers;

        public StringLexer(IList<ILexer<Terminal>> lexers)
        {
            if (lexers == null)
            {
                throw new ArgumentNullException("lexers");
            }

            this.lexers = lexers;
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, out TerminalString element)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException("scanner");
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
                        for (var j = elements.Count - 1; j >= 0; j--)
                        {
                            scanner.PutBack(elements[j].Values);
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