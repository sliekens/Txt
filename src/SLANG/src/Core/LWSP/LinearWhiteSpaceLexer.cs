// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LinearWhiteSpaceLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core.LWSP
{
    using System;

    [RuleName("LWSP")]
    public class LinearWhiteSpaceLexer : Lexer<LinearWhiteSpace>
    {
        private readonly ILexer<Repetition> linearWhiteSpaceRepetitionLexer;

        public LinearWhiteSpaceLexer(ILexer<Repetition> linearWhiteSpaceRepetitionLexer)
        {
            if (linearWhiteSpaceRepetitionLexer == null)
            {
                throw new ArgumentNullException("linearWhiteSpaceRepetitionLexer", "Precondition: linearWhiteSpaceRepetitionLexer != null");
            }

            this.linearWhiteSpaceRepetitionLexer = linearWhiteSpaceRepetitionLexer;
        }

        public override bool TryRead(ITextScanner scanner, out LinearWhiteSpace element)
        {
            Repetition sequence;
            if (this.linearWhiteSpaceRepetitionLexer.TryRead(scanner, out sequence))
            {
                element = new LinearWhiteSpace(sequence);
                return true;
            }

            element = default(LinearWhiteSpace);
            return false;
        }
    }
}