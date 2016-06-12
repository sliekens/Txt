using System;
using Txt.ABNF;
using Txt.Core;

namespace Sample1.expression
{
    public sealed class ExpressionLexer : Lexer<Expression>
    {
        private readonly ILexer<Concatenation> innerLexer;

        public ExpressionLexer(ILexer<Concatenation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }

            this.innerLexer = innerLexer;
        }

        protected override ReadResult<Expression> ReadImpl(ITextScanner scanner, ITextContext context)
        {
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<Expression>.FromResult(new Expression(result.Element));
            }
            return ReadResult<Expression>.FromSyntaxError(SyntaxError.FromReadResult(result, context));
        }
    }}
