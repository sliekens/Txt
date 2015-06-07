// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EndOfLine.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TextFx.ABNF.Core
{
    public class EndOfLine : Sequence
    {
        public EndOfLine(Sequence sequence)
            : base(sequence)
        {
        }
    }
}