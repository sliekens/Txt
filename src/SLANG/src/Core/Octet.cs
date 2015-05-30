// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Octet.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    /// <summary>Represents the OCTET rule: 8 bits of data. Unicode: U+0000 - U+00FF.</summary>
    public class Octet : Element
    {
        public Octet(Element element)
            : base(element)
        {
        }
    }
}