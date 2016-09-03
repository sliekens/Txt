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
        private readonly IList<ILexer<Element>> lexers;

        public ConcatenationLexer([NotNull] [ItemNotNull] params ILexer<Element>[] lexers)
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

        protected override IReadResult<Concatenation> ReadImpl(ITextScanner scanner, ITextContext context)
        {
            var stringBuilder = new StringBuilder();
            IList<Element> elements = new List<Element>(lexers.Count);
            var offset = scanner.StartRecording();
            try
            {
                // ReSharper disable once ForCanBeConvertedToForeach
                for (var i = 0; i < lexers.Count; i++)
                {
                    var readResult = lexers[i].Read(scanner);
                    if (readResult.IsSuccess)
                    {
                        stringBuilder.Append(readResult.Element.Text);
                        elements.Add(readResult.Element);
                    }
                    else
                    {
                        scanner.Seek(offset);
                        return ReadResult<Concatenation>.None;
                    }
                }
            }
            finally
            {
                scanner.StopRecording();
            }
            return ReadResult<Concatenation>.Success(new Concatenation(stringBuilder.ToString(), elements, context));
        }
    }
}
