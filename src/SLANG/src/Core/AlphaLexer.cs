// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AlphaLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
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
            this.element1Lexer = element1Lexer;
            this.element2Lexer = element2Lexer;
        }

        protected override Alpha CreateInstance1(Alpha.UpperCase element, ITextContext context)
        {
            return new Alpha(element, context);
        }

        protected override Alpha CreateInstance2(Alpha.LowerCase element, ITextContext context)
        {
            return new Alpha(element, context);
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

            protected override Alpha.UpperCase CreateInstance(Element element, ITextContext context)
            {
                return new Alpha.UpperCase(element);
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

            protected override Alpha.LowerCase CreateInstance(Element element, ITextContext context)
            {
                return new Alpha.LowerCase(element);
            }
        }
    }
}