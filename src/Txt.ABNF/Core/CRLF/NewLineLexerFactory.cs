﻿using System;
using System.Diagnostics;
using JetBrains.Annotations;
using Txt.ABNF.Core.CR;
using Txt.ABNF.Core.LF;
using Txt.Core;

namespace Txt.ABNF.Core.CRLF
{
    /// <summary>Creates instances of the <see cref="NewLineLexer" /> class.</summary>
    public class NewLineLexerFactory : ILexerFactory<NewLine>
    {
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly ILexer<CarriageReturn> carriageReturnLexer;

        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly ILexer<LineFeed> lineFeedLexer;

        /// <summary>
        /// </summary>
        /// <param name="concatenationLexerFactory"></param>
        /// <param name="carriageReturnLexer"></param>
        /// <param name="lineFeedLexer"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public NewLineLexerFactory(
            [NotNull] IConcatenationLexerFactory concatenationLexerFactory,
            [NotNull] ILexer<CarriageReturn> carriageReturnLexer,
            [NotNull] ILexer<LineFeed> lineFeedLexer)
        {
            if (concatenationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(concatenationLexerFactory));
            }
            if (carriageReturnLexer == null)
            {
                throw new ArgumentNullException(nameof(carriageReturnLexer));
            }
            if (lineFeedLexer == null)
            {
                throw new ArgumentNullException(nameof(lineFeedLexer));
            }
            this.concatenationLexerFactory = concatenationLexerFactory;
            this.carriageReturnLexer = carriageReturnLexer;
            this.lineFeedLexer = lineFeedLexer;
        }

        /// <inheritdoc />
        public ILexer<NewLine> Create()
        {
            var innerLexer = concatenationLexerFactory.Create(carriageReturnLexer, lineFeedLexer);
            return new NewLineLexer(innerLexer);
        }
    }
}
