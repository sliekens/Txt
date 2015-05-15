// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CarriageReturnLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    using Microsoft.Practices.ServiceLocation;

    public class CarriageReturnLexer : Lexer<CarriageReturn>
    {
        /// <summary>Initializes a new instance of the <see cref="CarriageReturnLexer"/> class.</summary>
        public CarriageReturnLexer(IServiceLocator serviceLocator)
            : base(serviceLocator, "CR")
        {
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, out CarriageReturn element)
        {
            Element carriageReturn;
            if (!TryReadTerminal(scanner, '\x0D', out carriageReturn))
            {
                element = default(CarriageReturn);
                return false;
            }

            element = new CarriageReturn(carriageReturn);
            return true;
        }
    }
}