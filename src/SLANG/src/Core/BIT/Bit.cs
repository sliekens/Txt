// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Bit.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SLANG.Core
{
    using System.Diagnostics;

    public class Bit : Alternative
    {
        public Bit(Alternative element)
            : base(element)
        {
        }

        public static explicit operator bool(Bit instance)
        {
            return instance.ToBool();
        }

        public bool ToBool()
        {
            Debug.Assert(this.Values == "0" || this.Values == "1", "this.Values == '0' || this.Values == '1'");
            if (this.Values == "1")
            {
                return true;
            }

            return false;
        }
    }
}