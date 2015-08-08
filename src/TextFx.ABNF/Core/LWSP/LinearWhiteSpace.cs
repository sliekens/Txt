// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LinearWhiteSpace.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TextFx.ABNF.Core
{
    public class LinearWhiteSpace : Repetition
    {
        public LinearWhiteSpace(Repetition repetition)
            : base(repetition)
        {
        }

        public override string GetWellFormedText()
        {
            // LWSP is optional, so don't return white space if there was no white space to begin with
            if (this.Text.Length == 0)
            {
                return string.Empty;
            }

            // Well-formed LWSP is exactly one (1) space
            return " ";
        }
    }
}