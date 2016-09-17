using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

        public override IEnumerable<Concatenation> ReadImpl(ITextScanner scanner, ITextContext context)
        {
            bool success = false;
            foreach (var concatenation in Branch(scanner, context, new List<Element>(lexers.Count)))
            {
                success = true;
                yield return concatenation;
            }

            if (!success)
            {
                scanner.Seek(context.Offset);
            }
        }

        private IEnumerable<Concatenation> Branch(
            ITextScanner scanner,
            ITextContext root,
            List<Element> elements)
        {
            if (elements.Count == lexers.Count)
            {
                yield return new Concatenation(string.Concat(elements), elements, root);
            }
            else
            {
                var next = lexers[elements.Count];
                var element = next.Read(scanner);
                if (element != null)
                {
                    var copy = new List<Element>(elements) { element };
                    foreach (var concat in Branch(scanner, root, copy))
                    {
                        yield return concat;
                    }
                }
            }
        }
    }
}
