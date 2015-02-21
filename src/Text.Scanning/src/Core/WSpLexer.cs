// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WSpLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   The w sp lexer.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Text.Scanning.Core
{
    using System.Diagnostics;
    using System.Diagnostics.Contracts;

    /// <summary>The w sp lexer.</summary>
    public class WSpLexer : Lexer<WSpToken>
    {
        /// <summary>The h tab lexer.</summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly HTabLexer hTabLexer;

        /// <summary>The sp lexer.</summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly SpLexer spLexer;

        /// <summary>Initializes a new instance of the <see cref="WSpLexer"/> class.</summary>
        public WSpLexer()
            : this(new SpLexer(), new HTabLexer())
        {
        }

        /// <summary>Initializes a new instance of the <see cref="WSpLexer"/> class.</summary>
        /// <param name="spLexer">The sp lexer.</param>
        /// <param name="hTabLexer">The h tab lexer.</param>
        public WSpLexer(SpLexer spLexer, HTabLexer hTabLexer)
        {
            Contract.Requires(spLexer != null);
            Contract.Requires(hTabLexer != null);
            this.spLexer = spLexer;
            this.hTabLexer = hTabLexer;
        }

        /// <inheritdoc />
        public override WSpToken Read(ITextScanner scanner)
        {
            WSpToken token;
            if (this.TryRead(scanner, out token))
            {
                return token;
            }

            throw new SyntaxErrorException(scanner.GetContext(), "Expected 'WSP'");
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, out WSpToken token)
        {
            if (scanner.EndOfInput)
            {
                token = default(WSpToken);
                return false;
            }

            var context = scanner.GetContext();
            SpToken spToken;
            HTabToken hTabToken;
            if (this.spLexer.TryRead(scanner, out spToken))
            {
                token = new WSpToken(spToken, context);
                return true;
            }

            if (this.hTabLexer.TryRead(scanner, out hTabToken))
            {
                token = new WSpToken(hTabToken, context);
                return true;
            }

            token = default(WSpToken);
            return false;
        }

        /// <summary>The object invariant.</summary>
        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.spLexer != null);
            Contract.Invariant(this.hTabLexer != null);
        }
    }
}