namespace TextFx.ABNF
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using JetBrains.Annotations;

    public class ConcatenationLexer : Lexer<Concatenation>
    {
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly IList<ILexer> lexers;

        public ConcatenationLexer([NotNull][ItemNotNull] params ILexer[] lexers)
        {
            if (lexers == null)
            {
                throw new ArgumentNullException(nameof(lexers));
            }
            if (lexers.Contains(null))
            {
                throw new ArgumentException("Collection contains null", nameof(lexers));
            }
            if (lexers.Length == 0)
            {
                throw new ArgumentException("Argument is empty collection", nameof(lexers));
            }
            this.lexers = lexers;
        }

        public override ReadResult<Concatenation> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var context = scanner.GetContext();
            IList<Element> elements = new List<Element>(lexers.Count);
            ReadResult<Element> lastResult = null;

            // ReSharper disable once ForCanBeConvertedToForeach
            for (var i = 0; i < lexers.Count; i++)
            {
                lastResult = lexers[i].ReadElement(scanner, lastResult?.Element);
                if (!lastResult.Success)
                {
                    break;
                }
                elements.Add(lastResult.Element);
            }
            if (elements.Count == lexers.Count)
            {
                return ReadResult<Concatenation>.FromResult(new Concatenation(elements, context));
            }
            if (elements.Count != 0)
            {
                for (var i = elements.Count - 1; i >= 0; i--)
                {
                    scanner.Unread(elements[i].Text);
                }
            }
            return ReadResult<Concatenation>.FromError(
                new SyntaxError
                {
                    Message = "A syntax error was found.",
                    InnerError = lastResult?.Error,
                    Context = context
                });
        }
    }
}
