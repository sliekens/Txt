// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CarriageReturnLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core.CR
{
    using System;

    public class CarriageReturnLexer : Lexer<CarriageReturn>
    {
        private readonly ILexer<Terminal> carriageReturnTerminalLexer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="carriageReturnTerminalLexer">%x0D</param>
        public CarriageReturnLexer(ILexer<Terminal> carriageReturnTerminalLexer)
            : base("CR")
        {
            if (carriageReturnTerminalLexer == null)
            {
                throw new ArgumentNullException("carriageReturnTerminalLexer", "Precondition: carriageReturnTerminalLexer != null");
            }

            this.carriageReturnTerminalLexer = carriageReturnTerminalLexer;
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, out CarriageReturn element)
        {
            Terminal result;
            if (this.carriageReturnTerminalLexer.TryRead(scanner, out result))
            {
                element = new CarriageReturn(result);
                return true;
            }

            element = default(CarriageReturn);
            return false;
        }
    }
}