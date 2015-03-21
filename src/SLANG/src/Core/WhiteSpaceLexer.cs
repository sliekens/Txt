// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WhiteSpaceLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    using System.Diagnostics;
    using System.Diagnostics.Contracts;

    public class WhiteSpaceLexer : AlternativeLexer<WhiteSpace, Space, HorizontalTab>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly SpaceLexer element1Lexer;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly HorizontalTabLexer element2Lexer;

        public WhiteSpaceLexer()
            : this(new SpaceLexer(), new HorizontalTabLexer())
        {
        }

        public WhiteSpaceLexer(SpaceLexer element1Lexer, HorizontalTabLexer element2Lexer)
            : base("WSP")
        {
            this.element1Lexer = element1Lexer;
            this.element2Lexer = element2Lexer;
        }

        protected override WhiteSpace CreateInstance1(Space element, ITextContext context)
        {
            return new WhiteSpace(element, context);
        }

        protected override WhiteSpace CreateInstance2(HorizontalTab element, ITextContext context)
        {
            return new WhiteSpace(element, context);
        }

        protected override bool TryRead1(ITextScanner scanner, out Space element)
        {
            return this.element1Lexer.TryRead(scanner, out element);
        }

        protected override bool TryRead2(ITextScanner scanner, out HorizontalTab element)
        {
            return this.element2Lexer.TryRead(scanner, out element);
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.element1Lexer != null);
            Contract.Invariant(this.element2Lexer != null);
        }
    }
}