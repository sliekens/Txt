// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WhiteSpaceLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   TODO
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    using System.Diagnostics;
    using System.Diagnostics.Contracts;

    /// <summary>TODO </summary>
    public class WhiteSpaceLexer : Lexer<WhiteSpace>
    {
        /// <summary>TODO </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly HorizontalTabLexer horizontalTabLexer;

        /// <summary>TODO </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly SpaceLexer spaceLexer;

        /// <summary>Initializes a new instance of the <see cref="WhiteSpaceLexer"/> class.</summary>
        public WhiteSpaceLexer()
            : this(new SpaceLexer(), new HorizontalTabLexer())
        {
        }

        /// <summary>Initializes a new instance of the <see cref="WhiteSpaceLexer"/> class.</summary>
        /// <param name="spaceLexer">TODO The space lexer.</param>
        /// <param name="horizontalTabLexer">TODO The horizontal tab lexer.</param>
        public WhiteSpaceLexer(SpaceLexer spaceLexer, HorizontalTabLexer horizontalTabLexer)
            : base("WSP")
        {
            Contract.Requires(spaceLexer != null);
            Contract.Requires(horizontalTabLexer != null);
            this.spaceLexer = spaceLexer;
            this.horizontalTabLexer = horizontalTabLexer;
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, out WhiteSpace element)
        {
            if (scanner.EndOfInput)
            {
                element = default(WhiteSpace);
                return false;
            }

            var context = scanner.GetContext();
            Space space;
            HorizontalTab horizontalTab;
            if (this.spaceLexer.TryRead(scanner, out space))
            {
                element = new WhiteSpace(space, context);
                return true;
            }

            if (this.horizontalTabLexer.TryRead(scanner, out horizontalTab))
            {
                element = new WhiteSpace(horizontalTab, context);
                return true;
            }

            element = default(WhiteSpace);
            return false;
        }

        /// <summary>TODO </summary>
        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.spaceLexer != null);
            Contract.Invariant(this.horizontalTabLexer != null);
        }
    }
}