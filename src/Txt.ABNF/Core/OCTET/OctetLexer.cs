using System.Collections.Generic;
using Txt.Core;

namespace Txt.ABNF.Core.OCTET
{
    public class OctetLexer : Lexer<Octet>
    {
        protected override IEnumerable<Octet> ReadImpl(ITextScanner scanner, ITextContext context)
        {
            var source = scanner.TextSource as IBinaryDataSource;
            if (source == null)
            {
                yield break;
            }
            var value = source.GetStream().ReadByte();
            if (value == -1)
            {
                yield break;
            }
            yield return new Octet(value, context);
        }
    }
}
