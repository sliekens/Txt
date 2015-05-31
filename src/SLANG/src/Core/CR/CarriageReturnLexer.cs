// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CarriageReturnLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    using System;

    public class CarriageReturnLexer : Lexer<CarriageReturn>
    {
        private readonly ILexer carriageReturnTerminalLexer;

        public CarriageReturnLexer(ILexer carriageReturnTerminalLexer)
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
            Element value;
            if (this.carriageReturnTerminalLexer.TryReadElement(scanner, out value))
            {
                element = new CarriageReturn(value);
                return true;
            }

            element = default(CarriageReturn);
            return false;
        }
    }
}