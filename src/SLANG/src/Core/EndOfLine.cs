// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EndOfLine.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    /// <summary>Represents the CRLF rule: 1 CR character followed by 1 LF character. Unicode: U+000D U+000A.</summary>
    public class EndOfLine : Sequence
    {
        public EndOfLine(Sequence sequence)
            : base(sequence)
        {
        }
    }
}