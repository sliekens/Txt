// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CarriageReturnLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    public class CarriageReturnLexer : Lexer<CarriageReturn>
    {
        /// <summary>Initializes a new instance of the <see cref="CarriageReturnLexer"/> class.</summary>
        public CarriageReturnLexer()
            : base("CR")
        {
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, out CarriageReturn element)
        {
            if (scanner.EndOfInput)
            {
                element = default(CarriageReturn);
                return false;
            }

            var context = scanner.GetContext();
            if (scanner.TryMatch('\x0D'))
            {
                element = new CarriageReturn(context);
                return true;
            }

            element = default(CarriageReturn);
            return false;
        }
    }
}