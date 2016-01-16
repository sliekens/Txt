// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CarriageReturnLexer.cs" company="Steven Liekens">
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

    public class CarriageReturnLexer : Lexer<CarriageReturn>
    {
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly ILexer<Terminal> innerLexer;

        /// <summary>
        /// </summary>
        /// <param name="innerLexer">%x0D</param>
        public CarriageReturnLexer([NotNull] ILexer<Terminal> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        public override ReadResult<CarriageReturn> Read(ITextScanner scanner)
        {
            var context = scanner.GetContext();
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<CarriageReturn>.FromResult(new CarriageReturn(result.Element));
            }
            return ReadResult<CarriageReturn>.FromSyntaxError(SyntaxError.FromReadResult(result, context));
        }
    }
}
