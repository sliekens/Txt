// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Bit.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SLANG.Core.BIT
{
    using System.Diagnostics;

    public class Bit : Alternative
    {
        public Bit(Element element)
            : base(element)
        {
        }

        public static explicit operator bool(Bit instance)
        {
            return instance.ToBool();
        }

        public bool ToBool()
        {
            Debug.Assert(this.Data == "0" || this.Data == "1", "this.Data == '0' || this.Data == '1'");
            if (this.Data == "1")
            {
                return true;
            }

            return false;
        }
    }
}