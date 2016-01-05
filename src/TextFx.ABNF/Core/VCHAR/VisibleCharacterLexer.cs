// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VisibleCharacterLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TextFx.ABNF.Core
{
    using System;
    using System.Diagnostics;

    public class VisibleCharacterLexer : Lexer<VisibleCharacter>
    {
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly ILexer<Terminal> innerLexer;

        /// <summary>
        /// </summary>
        /// <param name="innerLexer">%x21-7E</param>
        public VisibleCharacterLexer(ILexer<Terminal> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<VisibleCharacter> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            var context = scanner.GetContext();
            var result = this.innerLexer.Read(scanner, null);
            if (!result.Success)
            {
                return ReadResult<VisibleCharacter>.FromError(new SyntaxError
                {
                    Message = "Expected 'VCHAR'.",
                    RuleName = "VCHAR",
                    Context = context,
                    InnerError = result.Error
                });
            }

            var element = new VisibleCharacter(result.Element);
            if (previousElementOrNull != null)
            {
                element.PreviousElement = previousElementOrNull;
                previousElementOrNull.NextElement = element;
            }

            return ReadResult<VisibleCharacter>.FromResult(element);
        }
    }
}