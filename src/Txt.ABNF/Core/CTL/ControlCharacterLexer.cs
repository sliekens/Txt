// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ControlCharacterLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.CTL
{
    public class ControlCharacterLexer : Lexer<ControlCharacter>
    {
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly ILexer<Alternation> innerLexer;

        /// <summary>
        /// </summary>
        /// <param name="innerLexer">%x00-1F / %x7F</param>
        public ControlCharacterLexer([NotNull] ILexer<Alternation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        protected override ReadResult<ControlCharacter> ReadImpl(ITextScanner scanner, ITextContext context)
        {
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<ControlCharacter>.FromResult(new ControlCharacter(result.Element));
            }
            return ReadResult<ControlCharacter>.FromSyntaxError(SyntaxError.FromReadResult(result, context));
        }
    }
}
