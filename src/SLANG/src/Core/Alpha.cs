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
        public Alpha(UpperCase element, ITextContext context)
            : base(element, 1)
        {
        }

        public Alpha(LowerCase element, ITextContext context)
            : base(element, 2)
        {
        }
    }

    public partial class Alpha
    {
        public class UpperCase : Alternative
        {
            public UpperCase(char data, ITextContext context)
                : base(data, '\x41', '\x5A', context)
            {
            }
        }
    }

    public partial class Alpha
    {
        public class LowerCase : Alternative
        {
            public LowerCase(char data, ITextContext context)
                : base(data, '\x61', '\x7A', context)
            {
            }
        }
    }
}