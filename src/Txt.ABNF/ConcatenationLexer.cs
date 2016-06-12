using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF
{
    public class ConcatenationLexer : Lexer<Concatenation>
    {
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly IList<ILexer> lexers;

        public ConcatenationLexer([NotNull] [ItemNotNull] params ILexer[] lexers)
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

        protected override ReadResult<Concatenation> ReadImpl(ITextScanner scanner, ITextContext context)
        {
            var stringBuilder = new StringBuilder();
            IList<Element> elements = new List<Element>(lexers.Count);

            // ReSharper disable once ForCanBeConvertedToForeach
            for (var i = 0; i < lexers.Count; i++)
            {
                var readResult = lexers[i].ReadElement(scanner);
                if (readResult.Success)
                {
                    stringBuilder.Append(readResult.Text);
                    elements.Add(readResult.Element);
                }
                else
                {
                    var partialMatch = stringBuilder.ToString();
                    if (partialMatch.Length != 0)
                    {
                        scanner.Unread(partialMatch);
                    }
                    return
                        ReadResult<Concatenation>.FromSyntaxError(
                            new SyntaxError(
                                readResult.EndOfInput,
                                partialMatch,
                                readResult.ErrorText,
                                context,
                                readResult.Error));
                }
            }
            return ReadResult<Concatenation>.FromResult(new Concatenation(stringBuilder.ToString(), elements, context));
        }
    }
}
