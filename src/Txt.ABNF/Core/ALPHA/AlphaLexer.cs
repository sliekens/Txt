﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AlphaLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.ALPHA
{
    public class AlphaLexer : Lexer<Alpha>
    {
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly ILexer<Alternation> innerLexer;

        /// <summary>
        /// </summary>
        /// <param name="innerLexer">%x41-5A / %x61-7A</param>
        public AlphaLexer([NotNull] ILexer<Alternation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        protected override ReadResult<Alpha> ReadImpl(ITextScanner scanner, ITextContext context)
        {
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<Alpha>.FromResult(new Alpha(result.Element));
            }
            return ReadResult<Alpha>.FromSyntaxError(SyntaxError.FromReadResult(result, context));
        }
    }
}
