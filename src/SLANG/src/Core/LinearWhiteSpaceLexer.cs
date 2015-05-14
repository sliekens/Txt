// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LinearWhiteSpaceLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    using System.Collections.Generic;

    public partial class LinearWhiteSpaceLexer : RepetitionLexer<LinearWhiteSpace, LinearWhiteSpace.MultiLineWhiteSpace>
    {
        private readonly ILexer<LinearWhiteSpace.MultiLineWhiteSpace> elementLexer;

        public LinearWhiteSpaceLexer()
            : this(new EndOfLineLexer(), new WhiteSpaceLexer())
        {
        }

        public LinearWhiteSpaceLexer(ILexer<EndOfLine> endOfLineLexer, ILexer<WhiteSpace> whiteSpaceLexer)
            : this(new MultiLineWhiteSpaceLexer(whiteSpaceLexer, new MultiLineWhiteSpaceLexer.NewLineWhiteSpaceLexer(endOfLineLexer, whiteSpaceLexer)))
        {
        }

        public LinearWhiteSpaceLexer(ILexer<LinearWhiteSpace.MultiLineWhiteSpace> elementLexer)
            : base("LWSP", 0, int.MaxValue)
        {
            this.elementLexer = elementLexer;
        }

        protected override LinearWhiteSpace CreateInstance(IList<LinearWhiteSpace.MultiLineWhiteSpace> elements, int lowerBound, int upperBound, ITextContext context)
        {
            return new LinearWhiteSpace(elements, context);
        }

        protected override bool TryRead(ITextScanner scanner, int lowerBound, int upperBound, int current, out LinearWhiteSpace.MultiLineWhiteSpace element)
        {
            return this.elementLexer.TryRead(scanner, out element);
        }
    }

    public partial class LinearWhiteSpaceLexer
    {
        public partial class MultiLineWhiteSpaceLexer : AlternativeLexer<LinearWhiteSpace.MultiLineWhiteSpace, WhiteSpace, LinearWhiteSpace.MultiLineWhiteSpace.NewLineWhiteSpace>
        {
            private readonly ILexer<WhiteSpace> element1Lexer;

            private readonly ILexer<LinearWhiteSpace.MultiLineWhiteSpace.NewLineWhiteSpace> element2Lexer;

            public MultiLineWhiteSpaceLexer(ILexer<WhiteSpace> element1Lexer, ILexer<LinearWhiteSpace.MultiLineWhiteSpace.NewLineWhiteSpace> element2Lexer)
            {
                this.element1Lexer = element1Lexer;
                this.element2Lexer = element2Lexer;
            }

            protected override bool TryRead1(ITextScanner scanner, out WhiteSpace element)
            {
                return this.element1Lexer.TryRead(scanner, out element);
            }

            protected override bool TryRead2(ITextScanner scanner, out LinearWhiteSpace.MultiLineWhiteSpace.NewLineWhiteSpace element)
            {
                return this.element2Lexer.TryRead(scanner, out element);
            }
        }
    }

    public partial class LinearWhiteSpaceLexer
    {
        public partial class MultiLineWhiteSpaceLexer
        {
            public class NewLineWhiteSpaceLexer :
                SequenceLexer<LinearWhiteSpace.MultiLineWhiteSpace.NewLineWhiteSpace, EndOfLine, WhiteSpace>
            {
                private readonly ILexer<EndOfLine> element1Lexer;

                private readonly ILexer<WhiteSpace> element2Lexer;

                public NewLineWhiteSpaceLexer(ILexer<EndOfLine> element1Lexer, ILexer<WhiteSpace> element2Lexer)
                {
                    this.element1Lexer = element1Lexer;
                    this.element2Lexer = element2Lexer;
                }

                protected override LinearWhiteSpace.MultiLineWhiteSpace.NewLineWhiteSpace CreateInstance(EndOfLine element1, WhiteSpace element2, ITextContext context)
                {
                    return new LinearWhiteSpace.MultiLineWhiteSpace.NewLineWhiteSpace(element1, element2, context);
                }

                protected override bool TryRead1(ITextScanner scanner, out EndOfLine element)
                {
                    return this.element1Lexer.TryRead(scanner, out element);
                }

                protected override bool TryRead2(ITextScanner scanner, out WhiteSpace element)
                {
                    return this.element2Lexer.TryRead(scanner, out element);
                }
            }
        }
    }
}