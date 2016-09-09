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

        public override IEnumerable<Concatenation> Read2Impl(ITextScanner scanner, ITextContext context)
        {
            return Branch(scanner, context, context, new List<Element>(lexers.Count));
        }

        private IEnumerable<Concatenation> Branch(
            ITextScanner scanner,
            ITextContext root,
            ITextContext branch,
            List<Element> elements)
        {
            if (elements.Count == lexers.Count)
            {
                yield return new Concatenation(string.Concat(elements), elements, root);
            }
            else
            {
                var next = lexers[elements.Count];
                foreach (var element in next.Read(scanner, branch))
                {
                    var nextBranch = scanner.GetContext();
                    var copy = new List<Element>(elements) { element };
                    var success = false;
                    foreach (var concat in Branch(scanner, root, nextBranch, copy))
                    {
                        success = true;
                        yield return concat;
                    }
                    if (!success)
                    {
                        scanner.Seek(branch.Offset);
                    }
                }
            }
        }
    }
}
