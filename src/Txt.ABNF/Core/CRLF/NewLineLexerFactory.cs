using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.ABNF.Core.CR;
using Txt.ABNF.Core.LF;
using Txt.Core;

namespace Txt.ABNF.Core.CRLF
{
    public sealed class NewLineLexerFactory : RuleLexerFactory<NewLine>
    {
        static NewLineLexerFactory()
        {
            Default = new NewLineLexerFactory(
                CR.CarriageReturnLexerFactory.Default.Singleton(),
                LF.LineFeedLexerFactory.Default.Singleton());
        }

        public NewLineLexerFactory(
            [NotNull] ILexerFactory<CarriageReturn> carriageReturnLexerFactory,
            [NotNull] ILexerFactory<LineFeed> lineFeedLexerFactory)
        {
            if (carriageReturnLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(carriageReturnLexerFactory));
            }
            if (lineFeedLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(lineFeedLexerFactory));
            }
            CarriageReturnLexerFactory = carriageReturnLexerFactory;
            LineFeedLexerFactory = lineFeedLexerFactory;
        }

        [NotNull]
        public static NewLineLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<CarriageReturn> CarriageReturnLexerFactory { get; }

        [NotNull]
        public ILexerFactory<LineFeed> LineFeedLexerFactory { get; }

        public override ILexer<NewLine> Create()
        {
            var innerLexer = Concatenation.Create(CarriageReturnLexerFactory.Create(), LineFeedLexerFactory.Create());
            return new NewLineLexer(innerLexer);
        }
    }
}
