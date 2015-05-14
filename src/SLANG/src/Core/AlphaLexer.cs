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
        private readonly ILexer<Alpha.UpperCase> element1Lexer;

        private readonly ILexer<Alpha.LowerCase> element2Lexer;

        public AlphaLexer()
            : this(new UpperCaseLexer(), new LowerCaseLexer())
        {
        }

        public AlphaLexer(ILexer<Alpha.UpperCase> element1Lexer, ILexer<Alpha.LowerCase> element2Lexer)
            : base("ALPHA")
        {
            if (element1Lexer == null)
            {
                throw new ArgumentNullException("element1Lexer", "Precondition: element1Lexer != null");
            }

            if (element2Lexer == null)
            {
                throw new ArgumentNullException("element2Lexer", "Precondition: element2Lexer != null");
            }

            this.element1Lexer = element1Lexer;
            this.element2Lexer = element2Lexer;
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
            return this.element1Lexer.TryRead(scanner, out element);
        }

        protected override bool TryRead2(ITextScanner scanner, out Alpha.LowerCase element)
        {
            return this.element2Lexer.TryRead(scanner, out element);
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