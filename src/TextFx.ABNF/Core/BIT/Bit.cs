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
            Debug.Assert(this.Text == "0" || this.Text == "1", "this.Text == '0' || this.Text == '1'");
            if (this.Text == "1")
            {
                return true;
            }

            return false;
        }
    }
}