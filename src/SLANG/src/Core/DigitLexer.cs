// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DigitLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    using Microsoft.Practices.ServiceLocation;

    public class DigitLexer : AlternativeLexer<Digit>
    {
        /// <summary>Initializes a new instance of the <see cref="DigitLexer"/> class.</summary>
        public DigitLexer(IServiceLocator serviceLocator)
            : base(serviceLocator, "DIGIT", '\x30', '\x39')
        {
        }
    }
}