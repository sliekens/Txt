// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EndOfLine.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    using System.Diagnostics.Contracts;

    /// <summary>Represents the CRLF rule: 1 CR character followed by 1 LF character. Unicode: U+000D U+000A.</summary>
    public class EndOfLine : Sequence<CarriageReturn, LineFeed>
    {
        /// <summary>Initializes a new instance of the <see cref="T:SLANG.Core.EndOfLine"/> class with the specified
        /// characters and context.</summary>
        /// <param name="element1">The 'CR' component.</param>
        /// <param name="element2">The 'LF' component.</param>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        public EndOfLine(CarriageReturn element1, LineFeed element2, ITextContext context)
            : base(element1, element2, context)
        {
            Contract.Requires(element1 != null);
            Contract.Requires(element2 != null);
            Contract.Requires(context != null);
        }
    }
}