// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LinearWhiteSpace.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SLANG.Core.LWSP
{
    public class LinearWhiteSpace : Repetition
    {
        public LinearWhiteSpace(Repetition repetition)
            : base(repetition)
        {
        }

        public override string GetWellFormedData()
        {
            // LWSP is optional, so don't return white space if there was no white space to begin with
            if (this.Values.Length == 0)
            {
                return string.Empty;
            }

            // Well-formed LWSP is exactly one (1) space
            return " ";
        }
    }
}