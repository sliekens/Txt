using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
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

        protected override IReadResult<Repetition> ReadImpl(ITextScanner scanner, ITextContext context)
        {
            var stringBuilder = new StringBuilder();
            IList<Element> elements = new List<Element>(lowerBound);
            var offset = scanner.StartRecording();
            try
            {
                for (var i = 0; i < upperBound; i++)
                {
                    var readResult = lexer.Read(scanner);
                    if (readResult.IsSuccess)
                    {
                        elements.Add(readResult.Element);
                        stringBuilder.Append(readResult.Element.Text);
                    }
                    else
                    {
                        if (elements.Count < lowerBound)
                        {
                            scanner.Seek(offset);
                            return ReadResult<Repetition>.Fail(readResult.SyntaxError);
                        }
                        break;
                    }
                }
                return ReadResult<Repetition>.Success(new Repetition(stringBuilder.ToString(), elements, context));
            }
            finally
            {
                scanner.StopRecording();
            }
        }
    }
}
