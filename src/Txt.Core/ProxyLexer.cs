using System;
using System.Collections.Generic;
using System.Diagnostics;
using JetBrains.Annotations;

namespace Txt.Core
{
    public sealed class ProxyLexer<TElement> : Lexer<TElement>
        where TElement : Element
    {
        private ILexer<TElement> innerLexer;

        public bool Initialized { get; private set; }

        public void Initialize([NotNull] ILexer<TElement> lexer)
        {
            if (lexer == null)
            {
                throw new ArgumentNullException(nameof(lexer));
            }
            if (Initialized)
            {
                throw new InvalidOperationException(
                    "Initialize(ILexer`1) has already been called. Changing the rule after it has been initialized is not allowed.");
            }
            innerLexer = lexer;
            Initialized = true;
        }

        protected override IEnumerable<TElement> ReadImpl(ITextScanner scanner, ITextContext context)
        {
            if (!Initialized)
            {
                throw new InvalidOperationException("Initialize(ILexer`1) has never been called.");
            }
            Debug.Assert(innerLexer != null, "innerLexer != null");
            return innerLexer.Read(scanner, context);
        }
    }
}
