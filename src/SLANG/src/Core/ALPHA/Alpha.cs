// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Alpha.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core.ALPHA
{
    using System.Diagnostics;

    public class Alpha : Alternative
    {
        public Alpha(Alternative element)
            : base(element)
        {
        }

        public static explicit operator char(Alpha instance)
        {
            return instance.ToChar();
        }

        public char ToChar()
        {
            Debug.Assert(this.Element is Terminal, "this.Element is Terminal");
            return ((Terminal)this.Element).ToChar();
        }
    }
}