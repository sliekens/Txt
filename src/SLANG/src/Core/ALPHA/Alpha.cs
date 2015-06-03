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

        public char ToChar()
        {
            Debug.Assert(this.Data.Length == 1, "this.Data.Length == 1");
            return this.Data[0];
        }
    }
}