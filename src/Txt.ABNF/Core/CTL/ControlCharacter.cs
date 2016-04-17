// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ControlCharacter.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Jetbrains.Annotations;

namespace Txt.ABNF.Core.CTL
{
    public class ControlCharacter : Alternation
    {
        public ControlCharacter([NotNull] Alternation element)
            : base(element)
        {
        }
    }
}
