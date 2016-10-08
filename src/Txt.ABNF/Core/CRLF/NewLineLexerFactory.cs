using System;
using JetBrains.Annotations;
using Txt.ABNF.Core.CR;
using Txt.ABNF.Core.LF;
using Txt.Core;

namespace Txt.ABNF.Core.CRLF
{
    /// <summary>Creates instances of the <see cref="NewLineLexer" /> class.</summary>
    public class NewLineLexerFactory : LexerFactory<NewLine>
    {
        static NewLineLexerFactory()
        {
            Default = new NewLineLexerFactory(
                ABNF.ConcatenationLexerFactory.Default,
                CR.CarriageReturnLexerFactory.Default,
                LF.LineFeedLexerFactory.Default);
        }

        private NewLineLexerFactory(
            [NotNull] IConcatenationLexerFactory concatenationLexerFactory,
            [NotNull] ILexerFactory<CarriageReturn> carriageReturnLexerFactory,
            [NotNull] ILexerFactory<LineFeed> lineFeedLexerFactory)
        {
            if (concatenationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(concatenationLexerFactory));
            }
            if (carriageReturnLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(carriageReturnLexerFactory));
            }
            if (lineFeedLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(lineFeedLexerFactory));
            }
            ConcatenationLexerFactory = concatenationLexerFactory;
            CarriageReturnLexerFactory = carriageReturnLexerFactory;
            LineFeedLexerFactory = lineFeedLexerFactory;
        }

        [NotNull]
        public static NewLineLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<CarriageReturn> CarriageReturnLexerFactory { get; }

        [NotNull]
        public IConcatenationLexerFactory ConcatenationLexerFactory { get; }

        [NotNull]
        public ILexerFactory<LineFeed> LineFeedLexerFactory { get; set; }

        /// <inheritdoc />
        public override ILexer<NewLine> Create()
        {
            return
                new NewLineLexer(
                    ConcatenationLexerFactory.Create(
                        CarriageReturnLexerFactory.Create(),
                        LineFeedLexerFactory.Create()));
        }

        [NotNull]
        public NewLineLexerFactory UseCarriageReturnLexerFactory(
            [NotNull] ILexerFactory<CarriageReturn> carriageReturnLexerFactory)
        {
            if (carriageReturnLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(carriageReturnLexerFactory));
            }
            return new NewLineLexerFactory(
                ConcatenationLexerFactory,
                carriageReturnLexerFactory,
                LineFeedLexerFactory);
        }

        [NotNull]
        public NewLineLexerFactory UseConcatenationLexerFactory(
            [NotNull] IConcatenationLexerFactory concatenationLexerFactory)
        {
            if (concatenationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(concatenationLexerFactory));
            }
            return new NewLineLexerFactory(
                concatenationLexerFactory,
                CarriageReturnLexerFactory,
                LineFeedLexerFactory);
        }

        [NotNull]
        public NewLineLexerFactory UseLineFeedLexerFactory([NotNull] ILexerFactory<LineFeed> lineFeedLexerFactory)
        {
            if (lineFeedLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(lineFeedLexerFactory));
            }
            return new NewLineLexerFactory(
                ConcatenationLexerFactory,
                CarriageReturnLexerFactory,
                lineFeedLexerFactory);
        }
    }
}
