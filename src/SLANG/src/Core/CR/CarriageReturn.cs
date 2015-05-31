// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CarriageReturn.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    /// <summary>Represents the CR rule: 1 carriage return. Unicode: U+000D.</summary>
    public class CarriageReturn : Element
    {
        /// <summary>Initializes a new instance of the <see cref="T:SLANG.Core.CarriageReturn"/> class with a specified context.</summary>
        /// <param name="element">The carriage return element.</param>
        public CarriageReturn(Element element)
            : base(element)
        {
        }
    }
}