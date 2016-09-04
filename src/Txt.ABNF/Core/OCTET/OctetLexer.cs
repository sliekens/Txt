using Txt.Core;

namespace Txt.ABNF.Core.OCTET
{
    public class OctetLexer : Lexer<Octet>
    {
        protected override IReadResult<Octet> ReadImpl(ITextScanner scanner, ITextContext context)
        {
            if (scanner.Peek() == -1)
            {
                return ReadResult<Octet>.Fail(new SyntaxError(context, "Unexpected end of input"));
            }
            var octet = new Octet(scanner.Read(), context);
            return ReadResult<Octet>.Success(octet);
        }
    }
}
