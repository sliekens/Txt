namespace TextFx.ABNF
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    public class ConcatenationLexer : Lexer<Concatenation>
    {
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly IList<ILexer> lexers;

        public ConcatenationLexer(params ILexer[] lexers)
        {
            if (lexers == null)
            {
                throw new ArgumentNullException(nameof(lexers));
            }

            if (lexers.Length == 0)
            {
                throw new ArgumentException($"Precondition: {nameof(lexers)}.Count > 0", nameof(lexers));
            }

            // ReSharper disable once ForCanBeConvertedToForeach
            // ReSharper disable once LoopCanBeConvertedToQuery
            for (var i = 0; i < lexers.Length; i++)
            {
                var lexer = lexers[i];
                if (lexer == null)
                {
                    throw new ArgumentException($"Precondition: {nameof(lexers)}.All(lexer => lexer != null", nameof(lexers));
                }
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
            IList<Element> elements = new List<Element>(this.lexers.Count);
            ReadResult<Element> lastResult = null;

            // ReSharper disable once ForCanBeConvertedToForeach
            for (var i = 0; i < this.lexers.Count; i++)
            {
                lastResult = this.lexers[i].ReadElement(scanner, lastResult?.Element);
                if (!lastResult.Success)
                {
                    break;
                }

                elements.Add(lastResult.Element);
            }

            if (elements.Count == this.lexers.Count)
            {
                return ReadResult<Concatenation>.FromResult(new Concatenation(elements, context));
            }

            if (elements.Count != 0)
            {
                for (int i = elements.Count - 1; i >= 0; i--)
                {
                    scanner.Unread(elements[i].Text);
                }
            }

            return ReadResult<Concatenation>.FromError(new SyntaxError
            {
                Message = "A syntax error was found.",
                InnerError = lastResult?.Error,
                Context = context
            });
        }
    }
}