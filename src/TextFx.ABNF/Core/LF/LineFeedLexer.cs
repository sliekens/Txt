﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LineFeedLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TextFx.ABNF.Core
{
    using System;
    using System.Diagnostics;
    using JetBrains.Annotations;

    public class LineFeedLexer : Lexer<LineFeed>
    {
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly ILexer<Terminal> innerLexer;

        /// <summary>
        /// </summary>
        /// <param name="innerLexer">%x0A</param>
        public LineFeedLexer([NotNull] ILexer<Terminal> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        public override ReadResult<LineFeed> Read(ITextScanner scanner)
        {
            var context = scanner.GetContext();
            var result = innerLexer.Read(scanner);
            if (!result.Success)
            {
                return ReadResult<LineFeed>.FromError(
                    new SyntaxError
                    {
                        Message = "Expected 'LF'.",
                        RuleName = "LF",
                        Context = context,
                        InnerError = result.Error
                    });
            }
            return ReadResult<LineFeed>.FromResult(new LineFeed(result.Element));
        }
    }
}
