using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF
{
    public class AlternationLexer : Lexer<Alternation>
    {
        private readonly ILexer<Element>[] lexers;

        /// <summary>
        /// </summary>
        /// <param name="lexers"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public AlternationLexer([NotNull] [ItemNotNull] params ILexer<Element>[] lexers)
        {
            if (lexers == null)

            {
                throw new ArgumentNullException(nameof(lexers));
            }
            if (lexers.Length == 0)
            {
                throw new ArgumentException("Argument is empty collection", nameof(lexers));
            }
            if (lexers.Contains(null))
            {
                throw new ArgumentException("Collection contains null", nameof(lexers));
            }
            this.lexers = lexers;
        }

        protected override IEnumerable<Alternation> ReadImpl(ITextScanner scanner, ITextContext context)
        {
            for (var i = 0; i < lexers.Length; i++)
            {
                scanner.Seek(context.Offset);
                var element = lexers[i].Read(scanner);
                if (element != null)
                {
                    yield return new Alternation(element.Text, element, context, i + 1);
                }
            }
        }
    }
}
