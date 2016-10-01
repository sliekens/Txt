using System;
using JetBrains.Annotations;
using Txt.ABNF.Core.CRLF;
using Txt.ABNF.Core.WSP;
using Txt.Core;

namespace Txt.ABNF.Core.LWSP
{
    /// <summary>Creates instances of the <see cref="LinearWhiteSpaceLexer" /> class.</summary>
    public class LinearWhiteSpaceLexerFactory : ILexerFactory<LinearWhiteSpace>
    {
        private ILexer<LinearWhiteSpace> instance;

        static LinearWhiteSpaceLexerFactory()
        {
            Default = new LinearWhiteSpaceLexerFactory(
                ABNF.AlternationLexerFactory.Default,
                ABNF.ConcatenationLexerFactory.Default,
                ABNF.RepetitionLexerFactory.Default,
                WSP.WhiteSpaceLexerFactory.Default,
                CRLF.NewLineLexerFactory.Default);
        }

        /// <summary>
        /// </summary>
        /// <param name="alternationLexerFactory"></param>
        /// <param name="concatenationLexerFactory"></param>
        /// <param name="repetitionLexerFactory"></param>
        /// <param name="whiteSpaceLexerFactory"></param>
        /// <param name="newLineLexerFactory"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public LinearWhiteSpaceLexerFactory(
            [NotNull] IAlternationLexerFactory alternationLexerFactory,
            [NotNull] IConcatenationLexerFactory concatenationLexerFactory,
            [NotNull] IRepetitionLexerFactory repetitionLexerFactory,
            [NotNull] ILexerFactory<WhiteSpace> whiteSpaceLexerFactory,
            [NotNull] ILexerFactory<NewLine> newLineLexerFactory)
        {
            if (alternationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternationLexerFactory));
            }
            if (concatenationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(concatenationLexerFactory));
            }
            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(repetitionLexerFactory));
            }
            if (whiteSpaceLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(whiteSpaceLexerFactory));
            }
            if (newLineLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(newLineLexerFactory));
            }
            AlternationLexerFactory = alternationLexerFactory;
            ConcatenationLexerFactory = concatenationLexerFactory;
            RepetitionLexerFactory = repetitionLexerFactory;
            WhiteSpaceLexerFactory = whiteSpaceLexerFactory;
            NewLineLexerFactory = newLineLexerFactory;
        }

        [NotNull]
        public static LinearWhiteSpaceLexerFactory Default { get; }

        [NotNull]
        public IAlternationLexerFactory AlternationLexerFactory { get; }

        [NotNull]
        public IConcatenationLexerFactory ConcatenationLexerFactory { get; }

        [NotNull]
        public ILexerFactory<NewLine> NewLineLexerFactory { get; }

        [NotNull]
        public IRepetitionLexerFactory RepetitionLexerFactory { get; }

        [NotNull]
        public ILexerFactory<WhiteSpace> WhiteSpaceLexerFactory { get; }

        /// <inheritdoc />
        public ILexer<LinearWhiteSpace> Create()
        {
            var newLineLexer = NewLineLexerFactory.CreateOnce();
            var whiteSpaceLexer = WhiteSpaceLexerFactory.CreateOnce();
            var foldLexer = ConcatenationLexerFactory.Create(newLineLexer, whiteSpaceLexer);
            var breakingWhiteSpaceLexer = AlternationLexerFactory.Create(whiteSpaceLexer, foldLexer);
            var innerLexer = RepetitionLexerFactory.Create(breakingWhiteSpaceLexer, 0, int.MaxValue);
            return new LinearWhiteSpaceLexer(innerLexer);
        }

        public ILexer<LinearWhiteSpace> CreateOnce()
        {
            return instance ?? (instance = Create());
        }
    }
}
