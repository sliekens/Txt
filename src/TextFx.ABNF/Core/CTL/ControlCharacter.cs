// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ControlCharacter.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TextFx.ABNF.Core
{
    using JetBrains.Annotations;

    public class ControlCharacter : Alternative
    {
        public ControlCharacter([NotNull] Alternative element)
            : base(element)
        {
        }
    }
}
