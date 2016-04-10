// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Bit.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Diagnostics;
using Jetbrains.Annotations;

namespace Text.ABNF.Core.BIT
{
    public class Bit : Alternative
    {
        public Bit([NotNull] Alternative element)
            : base(element)
        {
        }

        public static explicit operator bool(Bit instance)
        {
            return instance.ToBool();
        }

        public bool ToBool()
        {
            Debug.Assert(Text == "0" || Text == "1", "this.Text == '0' || this.Text == '1'");
            if (Text == "1")
            {
                return true;
            }
            return false;
        }
    }
}
