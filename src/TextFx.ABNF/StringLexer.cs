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
        public override bool TryRead(ITextScanner scanner, Element previousElementOrNull, out TerminalString element)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException("scanner");
            }

            var context = scanner.GetContext();
            IList<Terminal> elements = new List<Terminal>(this.lexers.Count);
            Terminal lastResult = null;

            // ReSharper disable once ForCanBeConvertedToForeach
            for (var i = 0; i < this.lexers.Count; i++)
            {
                if (this.lexers[i].TryRead(scanner, lastResult, out lastResult))
                {
                    elements.Add(lastResult);
                }
                else
                {
                    while (lastResult != null)
                    {
                        scanner.Unread(lastResult.Value);
                        lastResult = (Terminal)lastResult.PreviousElement;
                    }

                    element = default(TerminalString);
                    return false;
                }
            }

            element = new TerminalString(elements, context);
            if (previousElementOrNull != null)
            {
                element.PreviousElement = previousElementOrNull;
                previousElementOrNull.NextElement = element;
            }

            return true;
        }
    }
}