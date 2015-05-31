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
        public CarriageReturn(Element element)
            : base(element)
        {
        }
    }
}