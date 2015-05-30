// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Alpha.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    /// <summary>Represents the ALPHA rule: 1 letter of the alphabet (case-insensitive). Unicode: U+0041-U+005A, U+0061-U+007A.</summary>
    public partial class Alpha : Alternative<Alpha.UpperCase, Alpha.LowerCase>
    {
        public Alpha(UpperCase element)
            : base(element, 1)
        {
        }

        public Alpha(LowerCase element)
            : base(element, 2)
        {
        }
    }

    public partial class Alpha
    {
        public class UpperCase : Element
        {
            public UpperCase(Element element)
                : base(element)
            {
            }
        }
    }

    public partial class Alpha
    {
        public class LowerCase : Element
        {
            public LowerCase(Element element)
                : base(element)
            {
            }
        }
    }
}