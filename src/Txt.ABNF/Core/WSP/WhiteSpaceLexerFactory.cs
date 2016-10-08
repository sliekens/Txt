using System;
using JetBrains.Annotations;
using Txt.ABNF.Core.HTAB;
using Txt.ABNF.Core.SP;
using Txt.Core;

namespace Txt.ABNF.Core.WSP
{
    /// <summary>Creates instances of the <see cref="WhiteSpaceLexer" /> class.</summary>
    public class WhiteSpaceLexerFactory : LexerFactory<WhiteSpace>
    {
        static WhiteSpaceLexerFactory()
        {
            Default = new WhiteSpaceLexerFactory(
                ABNF.AlternationLexerFactory.Default,
                SP.SpaceLexerFactory.Default,
                HTAB.HorizontalTabLexerFactory.Default);
        }

        /// <summary>
        /// </summary>
        /// <param name="alternationLexerFactory"></param>
        /// <param name="spaceLexerFactory"></param>
        /// <param name="horizontalTabLexerFactory"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public WhiteSpaceLexerFactory(
            [NotNull] IAlternationLexerFactory alternationLexerFactory,
            [NotNull] ILexerFactory<Space> spaceLexerFactory,
            [NotNull] ILexerFactory<HorizontalTab> horizontalTabLexerFactory)
        {
            if (alternationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternationLexerFactory));
            }
            if (spaceLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(spaceLexerFactory));
            }
            if (horizontalTabLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(horizontalTabLexerFactory));
            }
            AlternationLexerFactory = alternationLexerFactory;
            SpaceLexerFactory = spaceLexerFactory;
            HorizontalTabLexerFactory = horizontalTabLexerFactory;
        }

        [NotNull]
        public static WhiteSpaceLexerFactory Default { get; }

        [NotNull]
        public IAlternationLexerFactory AlternationLexerFactory { get; }

        [NotNull]
        public ILexerFactory<HorizontalTab> HorizontalTabLexerFactory { get; }

        [NotNull]
        public ILexerFactory<Space> SpaceLexerFactory { get; }

        /// <inheritdoc />
        public override ILexer<WhiteSpace> Create()
        {
            var innerLexer = AlternationLexerFactory.Create(
                SpaceLexerFactory.Create(),
                HorizontalTabLexerFactory.Create());
            return new WhiteSpaceLexer(innerLexer);
        }

        [NotNull]
        public WhiteSpaceLexerFactory UseAlternationLexerFactory(
            [NotNull] IAlternationLexerFactory alternationLexerFactory)
        {
            if (alternationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternationLexerFactory));
            }
            return new WhiteSpaceLexerFactory(
                alternationLexerFactory,
                SpaceLexerFactory,
                HorizontalTabLexerFactory);
        }

        [NotNull]
        public WhiteSpaceLexerFactory UseHorizontalTabLexerFactory(
            [NotNull] ILexerFactory<HorizontalTab> horizontalTabLexerFactory)
        {
            if (horizontalTabLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(horizontalTabLexerFactory));
            }
            return new WhiteSpaceLexerFactory(
                AlternationLexerFactory,
                SpaceLexerFactory,
                horizontalTabLexerFactory);
        }

        [NotNull]
        public WhiteSpaceLexerFactory UseSpaceLexerFactory([NotNull] ILexerFactory<Space> spaceLexerFactory)
        {
            if (spaceLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(spaceLexerFactory));
            }
            return new WhiteSpaceLexerFactory(
                AlternationLexerFactory,
                spaceLexerFactory,
                HorizontalTabLexerFactory);
        }
    }
}
