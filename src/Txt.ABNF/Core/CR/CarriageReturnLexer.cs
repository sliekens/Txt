// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CarriageReturnLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.CR
{
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

        protected override ReadResult<CarriageReturn> ReadImpl(ITextScanner scanner, ITextContext context)
        {
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return new ReadResult<CarriageReturn>(new CarriageReturn(result.Element));
            }
            return new ReadResult<CarriageReturn>(SyntaxError.FromReadResult(result, context));
        }
    }
}
