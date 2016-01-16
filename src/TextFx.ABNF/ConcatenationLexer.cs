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

        public override ReadResult<Concatenation> Read(ITextScanner scanner)
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
                lastResult = lexers[i].ReadElement(scanner);
                if (!lastResult.Success)
                {
                    break;
                }
                elements.Add(lastResult.Element);
            }
            var concatenation = string.Concat(elements.Select(element => element.Text));
            if (elements.Count == lexers.Count)
            {
                return ReadResult<Concatenation>.FromResult(new Concatenation(concatenation, elements, context));
            }
            if (concatenation.Length != 0)
            {
                scanner.Unread(concatenation);
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
