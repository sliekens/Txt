using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Txt.Core
{
    public sealed class ProxyLexer<TElement> : Lexer<TElement>
        where TElement : Element
    {
        private ILexer<TElement> innerLexer;

        public void Initialize([NotNull] ILexer<TElement> lexer)
        {
            if (lexer == null)
            {
                throw new ArgumentNullException(nameof(lexer));
            }
            if (innerLexer != null)
            {
                throw new InvalidOperationException(
                    "Initialize(ILexer`1) has already been called. Changing the rule after it has been initialized is not allowed.");
            }
            innerLexer = lexer;
        }

        public override IEnumerable<TElement> ReadImpl(ITextScanner scanner, ITextContext context)
        {
            if (innerLexer == null)
            {
                throw new InvalidOperationException("Initialize(ILexer`1) has never been called.");
            }
            return innerLexer.Read(scanner, context);
        }
    }
}
