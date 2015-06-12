// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Bit.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TextFx.ABNF.Core
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
            Debug.Assert(this.Value == "0" || this.Value == "1", "this.Value == '0' || this.Value == '1'");
            if (this.Value == "1")
            {
                return true;
            }

            return false;
        }
    }
}