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

    public partial class CarriageReturnLexer : Lexer<CarriageReturn>
    {
        private readonly ILexer<Element> carriageReturnTerminaLexer;

        /// <summary>Initializes a new instance of the <see cref="CarriageReturnLexer"/> class.</summary>
        public CarriageReturnLexer(ILexer<Element> carriageReturnTerminaLexer)
            : base("CR")
        {
            if (carriageReturnTerminaLexer == null)
            {
                throw new ArgumentNullException("carriageReturnTerminaLexer", "Precondition: carriageReturnTerminaLexer != null");
            }

            this.carriageReturnTerminaLexer = carriageReturnTerminaLexer;
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, out CarriageReturn element)
        {
            Element value;
            if (this.carriageReturnTerminaLexer.TryRead(scanner, out value))
            {
                element = new CarriageReturn(value);
                return true;
            }

            element = default(CarriageReturn);
            return false;
        }
    }

    public partial class CarriageReturnLexer
    {
        public class CarriageReturnTerminalLexer : TerminalsLexer
        {
            public CarriageReturnTerminalLexer()
                : base('\x0D')
            {
            }
        }
    }
}