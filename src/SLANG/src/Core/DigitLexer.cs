// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DigitLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    public class DigitLexer : AlternativeLexer<Digit>
    {
        /// <summary>Initializes a new instance of the <see cref="DigitLexer"/> class.</summary>
        public DigitLexer()
            : base("DIGIT", '\x30', '\x39')
        {
        }
    }
}