// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LinearWhiteSpace.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    using System.Collections.Generic;

    /// <summary>Represents the LWSP rule: any linear white space. The LWSP rule permits lines containing only white space.</summary>
    public partial class LinearWhiteSpace : Repetition<LinearWhiteSpace.MultiLineWhiteSpace>
    {
        public LinearWhiteSpace(IList<MultiLineWhiteSpace> elements, ITextContext context)
            : base(elements, context)
        {
        }
    }

    public partial class LinearWhiteSpace
    {
        public partial class MultiLineWhiteSpace : Alternative<WhiteSpace, MultiLineWhiteSpace.NewLineWhiteSpace>
        {
            public MultiLineWhiteSpace(Element element, int alternative, ITextContext context)
                : base(element, alternative, context)
            {
            }
        }
    }

    public partial class LinearWhiteSpace
    {
        public partial class MultiLineWhiteSpace
        {
            public class NewLineWhiteSpace : Sequence<EndOfLine, WhiteSpace>
            {
                public NewLineWhiteSpace(EndOfLine element1, WhiteSpace element2, ITextContext context)
                    : base(element1, element2, context)
                {
                }
            }
        }
    }
}