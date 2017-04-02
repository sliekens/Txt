using System;
using JetBrains.Annotations;
using Txt.ABNF.Core.HTAB;
using Txt.ABNF.Core.SP;
using Txt.Core;

namespace Txt.ABNF.Core.WSP
{
    public sealed class WhiteSpaceLexerFactory : LexerFactory<WhiteSpace>
    {
        static WhiteSpaceLexerFactory()
        {
            Default = new WhiteSpaceLexerFactory(
                SP.SpaceLexerFactory.Default.Singleton(),
                HTAB.HorizontalTabLexerFactory.Default.Singleton());
        }

        public WhiteSpaceLexerFactory(
            [NotNull] ILexerFactory<Space> spaceLexerFactory,
            [NotNull] ILexerFactory<HorizontalTab> horizontalTabLexerFactory)
        {
            if (spaceLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(spaceLexerFactory));
            }
            if (horizontalTabLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(horizontalTabLexerFactory));
            }
            HorizontalTabLexerFactory = horizontalTabLexerFactory;
            SpaceLexerFactory = spaceLexerFactory;
        }

        [NotNull]
        public static WhiteSpaceLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<HorizontalTab> HorizontalTabLexerFactory { get; }

        [NotNull]
        public ILexerFactory<Space> SpaceLexerFactory { get; }

        public override ILexer<WhiteSpace> Create()
        {
            return new WhiteSpaceLexer(SpaceLexerFactory.Create(), HorizontalTabLexerFactory.Create());
        }
    }
}
