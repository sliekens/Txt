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
    using System.Diagnostics.Contracts;

    /// <summary>Represents the OCTET rule: 8 bits of data. Unicode: U+0000 - U+00FF.</summary>
    public class Octet : Alternative
    {
        public Octet(char data, ITextContext context)
            : base(data, '\0', '\xFF', context)
        {
            Contract.Requires(context != null);
        }
    }
}