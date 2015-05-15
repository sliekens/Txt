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
    public partial class Alpha : Alternative<Alpha.Uppercase, Alpha.Lowercase>
    {
        public Alpha(Uppercase element)
            : base(element, 1)
        {
        }

        public Alpha(Lowercase element)
            : base(element, 2)
        {
        }
    }

    public partial class Alpha
    {
        public class Uppercase : Alternative
        {
            public Uppercase(Element element)
                : base(element, '\x41', '\x5A')
            {
            }
        }
    }

    public partial class Alpha
    {
        public class Lowercase : Alternative
        {
            public Lowercase(Element element)
                : base(element, '\x61', '\x7A')
            {
            }
        }
    }
}