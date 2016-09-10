using System;
using System.Collections.Generic;
using System.Diagnostics;
using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF
{
    /// <summary>Provides the base class for lexers whose lexer rule is a repetition of elements.</summary>
    public class RepetitionLexer : Lexer<Repetition>
    {
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly ILexer<Element> lexer;

        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly int lowerBound;

        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly int upperBound;

        /// <summary>
        ///     Initializes a new instance of the <see cref="RepetitionLexer" /> class with a specified lower and upper bound,
        ///     both inclusive.
        /// </summary>
        /// <param name="lexer">The lexer for the repeating element.</param>
        /// <param name="lowerBound">A number that indicates the minimum number of occurrences (inclusive).</param>
        /// <param name="upperBound">A number that indicates the maximum number of occurrences (inclusive).</param>
        public RepetitionLexer([NotNull] ILexer<Element> lexer, int lowerBound, int upperBound)
        {
            if (lexer == null)
            {
                throw new ArgumentNullException(nameof(lexer));
            }
            if (lowerBound < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(lowerBound), "Precondition: lowerBound >= 0");
            }
            if (upperBound < lowerBound)
            {
                throw new ArgumentOutOfRangeException(nameof(upperBound), "Precondition: upperBound >= lowerBound");
            }
            this.lexer = lexer;
            this.lowerBound = lowerBound;
            this.upperBound = upperBound;
        }

        public override IEnumerable<Repetition> Read2Impl(ITextScanner scanner, ITextContext context)
        {
            bool success = false;
            foreach (var repetition in Branch(scanner, context, context, new List<Element>(lowerBound)))
            {
                success = true;
                yield return repetition;
            }
            if (!success)
            {
                scanner.Seek(context.Offset);
            }
        }

        private IEnumerable<Repetition> Branch(
            ITextScanner scanner,
            ITextContext root,
            ITextContext branch,
            List<Element> elements)
        {
            if (elements.Count >= lowerBound)
            {
                yield return new Repetition(string.Concat(elements), elements, root);
                if (elements.Count == upperBound)
                {
                    yield break;
                }
            }
            foreach (var element in lexer.Read(scanner, branch))
            {
                var copy = new List<Element>(elements) { element };
                var nextBranch = scanner.GetContext();
                foreach (var repetition in Branch(scanner, root, nextBranch, copy))
                {
                    yield return repetition;
                }
            }
        }
    }
}
