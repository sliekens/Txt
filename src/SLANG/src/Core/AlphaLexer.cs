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
        private readonly ILexer<Alpha.UpperCase> upperCaseLexer;

        private readonly ILexer<Alpha.LowerCase> lowerCaseLexer;

        public AlphaLexer()
            : this(new UpperCaseLexer(), new LowerCaseLexer())
        {
        }

        public AlphaLexer(ILexer<Alpha.UpperCase> upperCaseLexer, ILexer<Alpha.LowerCase> lowerCaseLexer)
            : base("ALPHA")
        {
            if (upperCaseLexer == null)
            {
                throw new ArgumentNullException("upperCaseLexer", "Precondition: upperCaseLexer != null");
            }

            if (lowerCaseLexer == null)
            {
                throw new ArgumentNullException("lowerCaseLexer", "Precondition: lowerCaseLexer != null");
            }

            this.upperCaseLexer = upperCaseLexer;
            this.lowerCaseLexer = lowerCaseLexer;
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
            return this.upperCaseLexer.TryRead(scanner, out element);
        }

        protected override bool TryRead2(ITextScanner scanner, out Alpha.LowerCase element)
        {
            return this.lowerCaseLexer.TryRead(scanner, out element);
        }
    }

    public partial class AlphaLexer
    {
        public class UpperCaseLexer : AlternativeLexer<Alpha.UpperCase>
        {
            public UpperCaseLexer()
                : base('\x41', '\x5A')
            {
            }
        }
    }

    public partial class AlphaLexer
    {
        public class LowerCaseLexer : AlternativeLexer<Alpha.LowerCase>
        {
            public LowerCaseLexer()
                : base('\x61', '\x7A')
            {
            }
        }
    }
}