using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.ABNF.Core.CRLF;
using Txt.ABNF.Core.WSP;
using Txt.Core;

namespace Txt.ABNF.Core.LWSP
{
    public sealed class LinearWhiteSpaceLexerFactory : RuleLexerFactory<LinearWhiteSpace>
    {
        static LinearWhiteSpaceLexerFactory()
        {
            Default = new LinearWhiteSpaceLexerFactory(
                WSP.WhiteSpaceLexerFactory.Default.Singleton(),
                CRLF.NewLineLexerFactory.Default.Singleton());
        }

        public LinearWhiteSpaceLexerFactory(
            [NotNull] ILexerFactory<WhiteSpace> whiteSpaceLexerFactory,
            [NotNull] ILexerFactory<NewLine> newLineLexerFactory)
        {
            if (whiteSpaceLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(whiteSpaceLexerFactory));
            }
            if (newLineLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(newLineLexerFactory));
            }
            WhiteSpaceLexerFactory = whiteSpaceLexerFactory;
            NewLineLexerFactory = newLineLexerFactory;
        }

        [NotNull]
        public static LinearWhiteSpaceLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<NewLine> NewLineLexerFactory { get; }

        [NotNull]
        public ILexerFactory<WhiteSpace> WhiteSpaceLexerFactory { get; }

        public override ILexer<LinearWhiteSpace> Create()
        {
            var wsp = WhiteSpaceLexerFactory.Create();
            var crlf = NewLineLexerFactory.Create();
            var innerLexer = Repetition.Create(
                Alternation.Create(
                    wsp,
                    Concatenation.Create(
                        crlf,
                        wsp)),
                0,
                int.MaxValue);
            return new LinearWhiteSpaceLexer(innerLexer);
        }
    }
}
