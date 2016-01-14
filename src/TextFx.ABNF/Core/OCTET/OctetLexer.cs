// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OctetLexer.cs" company="Steven Liekens">
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

    public class OctetLexer : Lexer<Octet>
    {
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly ILexer<Terminal> innerLexer;

        public OctetLexer([NotNull] ILexer<Terminal> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        public override ReadResult<Octet> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            var context = scanner.GetContext();
            var result = innerLexer.Read(scanner, null);
            if (!result.Success)
            {
                return ReadResult<Octet>.FromError(
                    new SyntaxError
                    {
                        Message = "Expected 'OCTET'.",
                        RuleName = "OCTET",
                        Context = context,
                        InnerError = result.Error
                    });
            }
            var element = new Octet(result.Element);
            if (previousElementOrNull != null)
            {
                element.PreviousElement = previousElementOrNull;
                previousElementOrNull.NextElement = element;
            }
            return ReadResult<Octet>.FromResult(element);
        }
    }
}
