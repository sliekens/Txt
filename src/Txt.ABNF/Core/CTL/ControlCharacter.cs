// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ControlCharacter.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Jetbrains.Annotations;

namespace Text.ABNF.Core.CTL
{
    public class ControlCharacter : Alternative
    {
        public ControlCharacter([NotNull] Alternative element)
            : base(element)
        {
        }
    }
}
