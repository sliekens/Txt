using Txt.Core;

namespace Txt.ABNF.Core.OCTET
{
    public class OctetLexer : Lexer<Octet>
    {
        protected override IReadResult<Octet> ReadImpl(ITextScanner scanner, ITextContext context)
        {
            var read = scanner.Read();
            if (read != -1)
            {
                return new ReadResult<Octet>(new Octet(read, context));
            }
            return new ReadResult<Octet>(new SyntaxError(true, "", "", context));
        }
    }
}
