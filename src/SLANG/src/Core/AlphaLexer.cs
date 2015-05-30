// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AlphaLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    using System;

    public partial class AlphaLexer : AlternativeLexer<Alpha, Alpha.UpperCase, Alpha.LowerCase>
    {
        private readonly ILexer<Element> upperCaseValueRangeLexer;

        private readonly ILexer<Element> lowerCaseValueRangeLexer;

        public AlphaLexer(ILexer<Element> upperCaseValueRangeLexer, ILexer<Element> lowerCaseValueRangeLexer)
            : base("ALPHA")
        {
            if (upperCaseValueRangeLexer == null)
            {
                throw new ArgumentNullException("upperCaseValueRangeLexer", "Precondition: upperCaseValueRangeLexer != null");
            }

            if (lowerCaseValueRangeLexer == null)
            {
                throw new ArgumentNullException("lowerCaseValueRangeLexer", "Precondition: lowerCaseValueRangeLexer != null");
            }

            this.upperCaseValueRangeLexer = upperCaseValueRangeLexer;
            this.lowerCaseValueRangeLexer = lowerCaseValueRangeLexer;
        }

        protected override Alpha CreateInstance1(Alpha.UpperCase element)
        {
            return new Alpha(element);
        }

        protected override Alpha CreateInstance2(Alpha.LowerCase element)
        {
            return new Alpha(element);
        }

        protected override bool TryRead1(ITextScanner scanner, out Alpha.UpperCase element)
        {
            Element value;
            if (this.upperCaseValueRangeLexer.TryRead(scanner, out value))
            {
                element = new Alpha.UpperCase(value);
                return true;
            }

            element = default(Alpha.UpperCase);
            return false;
        }

        protected override bool TryRead2(ITextScanner scanner, out Alpha.LowerCase element)
        {
            Element value;
            if (this.lowerCaseValueRangeLexer.TryRead(scanner, out value))
            {
                element = new Alpha.LowerCase(value);
                return true;
            }

            element = default(Alpha.LowerCase);
            return false;
        }
    }

    public partial class AlphaLexer
    {
        public class UpperCaseValueRangeLexer : ValueRangeLexer
        {
            public UpperCaseValueRangeLexer()
                : base('\x41', '\x5A')
            {
            }
        }
    }

    public partial class AlphaLexer
    {
        public class LowerCaseValueRangeLexer : ValueRangeLexer
        {
            public LowerCaseValueRangeLexer()
                : base('\x61', '\x7A')
            {
            }
        }
    }
}