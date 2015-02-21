// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CrLfLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   The cr lf lexer.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Text.Scanning.Core
{
    using System.Diagnostics;
    using System.Diagnostics.Contracts;

    /// <summary>The cr lf lexer.</summary>
    public class CrLfLexer : Lexer<CrLfToken>
    {
        /// <summary>The cr lexer.</summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ILexer<CrToken> crLexer;

        /// <summary>The lf lexer.</summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ILexer<LfToken> lfLexer;

        /// <summary>Initializes a new instance of the <see cref="CrLfLexer"/> class.</summary>
        public CrLfLexer()
            : this(new CrLexer(), new LfLexer())
        {
        }

        /// <summary>Initializes a new instance of the <see cref="CrLfLexer"/> class.</summary>
        /// <param name="crLexer">The cr lexer.</param>
        /// <param name="lfLexer">The lf lexer.</param>
        public CrLfLexer(ILexer<CrToken> crLexer, ILexer<LfToken> lfLexer)
        {
            Contract.Requires(crLexer != null);
            Contract.Requires(lfLexer != null);
            this.crLexer = crLexer;
            this.lfLexer = lfLexer;
        }

        /// <inheritdoc />
        public override CrLfToken Read(ITextScanner scanner)
        {
            var context = scanner.GetContext();
            try
            {
                var crToken = this.crLexer.Read(scanner);
                var lfToken = this.lfLexer.Read(scanner);
                Contract.Assume(lfToken.Offset == crToken.Offset + 1);
                return new CrLfToken(crToken, lfToken, context);
            }
            catch (SyntaxErrorException syntaxErrorException)
            {
                throw new SyntaxErrorException(context, "Expected 'CRLF'", syntaxErrorException);
            }
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, out CrLfToken token)
        {
            var context = scanner.GetContext();
            CrToken crToken;
            if (scanner.EndOfInput || !this.crLexer.TryRead(scanner, out crToken))
            {
                token = default(CrLfToken);
                return false;
            }

            LfToken lfToken;
            if (scanner.EndOfInput || !this.lfLexer.TryRead(scanner, out lfToken))
            {
                this.crLexer.PutBack(scanner, crToken);
                token = default(CrLfToken);
                return false;
            }

            Contract.Assume(lfToken.Offset == crToken.Offset + 1);
            token = new CrLfToken(crToken, lfToken, context);
            return true;
        }

        /// <summary>The object invariant.</summary>
        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.crLexer != null);
            Contract.Invariant(this.lfLexer != null);
        }
    }
}