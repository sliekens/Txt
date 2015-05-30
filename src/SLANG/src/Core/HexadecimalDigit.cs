// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HexadecimalDigit.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    using System;

    /// <summary>
    /// Represents the HEXDIG rule: 1 hexadecimal digit (case-insensitive). Unicode: U+0030 - U+0039, U+0041 - U+0046,
    /// U+0061 - U+0066.
    /// </summary>
    public partial class HexadecimalDigit : Alternative<Digit, HexadecimalDigit.A, HexadecimalDigit.B, HexadecimalDigit.C, HexadecimalDigit.D, HexadecimalDigit.E, HexadecimalDigit.F>
    {
        /// <summary>Initializes a new instance of the <see cref="HexadecimalDigit"/> class with a specified digit.</summary>
        /// <param name="digit">The hexadecimal digit.</param>
        public HexadecimalDigit(Digit digit)
            : base(digit, 1)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="HexadecimalDigit"/> class with a specified digit.</summary>
        /// <param name="digit">The hexadecimal digit.</param>
        public HexadecimalDigit(A digit)
            : base(digit, 2)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="HexadecimalDigit"/> class with a specified digit.</summary>
        /// <param name="digit">The hexadecimal digit.</param>
        public HexadecimalDigit(B digit)
            : base(digit, 3)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="HexadecimalDigit"/> class with a specified digit.</summary>
        /// <param name="digit">The hexadecimal digit.</param>
        public HexadecimalDigit(C digit)
            : base(digit, 4)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="HexadecimalDigit"/> class with a specified digit.</summary>
        /// <param name="digit">The hexadecimal digit.</param>
        public HexadecimalDigit(D digit)
            : base(digit, 5)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="HexadecimalDigit"/> class with a specified digit.</summary>
        /// <param name="digit">The hexadecimal digit.</param>
        public HexadecimalDigit(E digit)
            : base(digit, 6)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="HexadecimalDigit"/> class with a specified digit.</summary>
        /// <param name="digit">The hexadecimal digit.</param>
        public HexadecimalDigit(F digit)
            : base(digit, 7)
        {
        }
    }

    public partial class HexadecimalDigit
    {
        public class A : Element
        {
            public A(Element element)
                : base(element)
            {
            }
        }
    }

    public partial class HexadecimalDigit
    {
        public class B : Element
        {
            public B(Element element)
                : base(element)
            {
            }
        }
    }

    public partial class HexadecimalDigit
    {
        public class C : Element
        {
            public C(Element element)
                : base(element)
            {
            }
        }
    }

    public partial class HexadecimalDigit
    {
        public class D : Element
        {
            public D(Element element)
                : base(element)
            {
            }
        }
    }

    public partial class HexadecimalDigit
    {
        public class E : Element
        {
            public E(Element element)
                : base(element)
            {
            }
        }
    }

    public partial class HexadecimalDigit
    {
        public class F : Element
        {
            public F(Element element)
                : base(element)
            {
            }
        }
    }
}