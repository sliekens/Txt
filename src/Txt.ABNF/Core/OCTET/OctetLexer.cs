using System.Collections.Generic;
using Txt.Core;

namespace Txt.ABNF.Core.OCTET
{
    public class OctetLexer : Lexer<Octet>
    {
        public override IEnumerable<Octet> Read2Impl(ITextScanner scanner, ITextContext context)
        {
            var value = scanner.Read();
            if (value == -1)
            {
                yield break;
            }
            yield return new Octet(value, context);
        }
    }
}
