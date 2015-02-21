// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HexDigLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   The hex dig lexer.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Text.Scanning.Core
{
    using System.Diagnostics;
    using System.Diagnostics.Contracts;

    /// <summary>The hex dig lexer.</summary>
    public class HexDigLexer : Lexer<HexDigToken>
    {
        /// <summary>The digit lexer.</summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ILexer<DigitToken> digitLexer;

        /// <summary>Initializes a new instance of the <see cref="HexDigLexer"/> class.</summary>
        public HexDigLexer()
            : this(new DigitLexer())
        {
        }

        /// <summary>Initializes a new instance of the <see cref="HexDigLexer"/> class.</summary>
        /// <param name="digitLexer">The digit lexer.</param>
        public HexDigLexer(ILexer<DigitToken> digitLexer)
        {
            Contract.Requires(digitLexer != null);
            this.digitLexer = digitLexer;
        }

        /// <inheritdoc />
        public override HexDigToken Read(ITextScanner scanner)
        {
            var context = scanner.GetContext();
            HexDigToken token;
            if (this.TryRead(scanner, out token))
            {
                return token;
            }

            throw new SyntaxErrorException(context, "Expected 'HEXDIG'");
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, out HexDigToken token)
        {
            if (scanner.EndOfInput)
            {
                token = default(HexDigToken);
                return false;
            }

            var context = scanner.GetContext();
            DigitToken digitToken;
            if (this.digitLexer.TryRead(scanner, out digitToken))
            {
                token = new HexDigToken(digitToken, context);
                return true;
            }

            // A-F
            for (var c = 'A'; c <= 'F'; c++)
            {
                if (scanner.TryMatch(c))
                {
                    token = new HexDigToken(c, context);
                    return true;
                }
            }

            // a-f
            for (var c = 'a'; c <= 'f'; c++)
            {
                if (scanner.TryMatch(c))
                {
                    token = new HexDigToken(c, context);
                    return true;
                }
            }

            token = default(HexDigToken);
            return false;
        }

        /// <summary>The object invariant.</summary>
        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.digitLexer != null);
        }
    }
}