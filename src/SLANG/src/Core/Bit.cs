// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Bit.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    using System;

    /// <summary>Represents the BIT rule: "0" / "1"</summary>
    public partial class Bit : Alternative<Bit.Zero, Bit.One>
    {
        public Bit(Zero bit)
            : base(bit, 1)
        {
        }

        public Bit(One bit)
            : base(bit, 2)
        {
        }
    }

    public partial class Bit
    {
        public class Zero : Element
        {
            public Zero(Element element)
                : base(element)
            {
                if (element.Data != "0")
                {
                    throw new ArgumentOutOfRangeException("element", element, "Precondition: element.Data == \"0\"");
                }
            }
        }
    }

    public partial class Bit
    {
        public class One : Element
        {
            public One(Element element)
                : base(element)
            {
                if (element.Data != "1")
                {
                    throw new ArgumentOutOfRangeException("element", element, "Precondition: element.Data == \"1\"");
                }
            }
        }
    }
}