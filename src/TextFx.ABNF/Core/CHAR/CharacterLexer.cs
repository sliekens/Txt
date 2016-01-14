// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CharacterLexer.cs" company="Steven Liekens">
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

    public class CharacterLexer : Lexer<Character>
    {
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly ILexer<Terminal> innerLexer;

        /// <summary>
        /// </summary>
        /// <param name="innerLexer">%x01-7F</param>
        public CharacterLexer([NotNull] ILexer<Terminal> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        public override ReadResult<Character> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            var context = scanner.GetContext();
            var result = innerLexer.Read(scanner, null);
            if (!result.Success)
            {
                return ReadResult<Character>.FromError(
                    new SyntaxError
                    {
                        Message = "Expected 'CHAR'.",
                        RuleName = "CHAR",
                        Context = context,
                        InnerError = result.Error
                    });
            }
            var element = new Character(result.Element);
            if (previousElementOrNull != null)
            {
                element.PreviousElement = previousElementOrNull;
                previousElementOrNull.NextElement = element;
            }
            return ReadResult<Character>.FromResult(element);
        }
    }
}
