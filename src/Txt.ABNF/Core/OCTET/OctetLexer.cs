using Txt.Core;

namespace Txt.ABNF.Core.OCTET
{
    public class OctetLexer : Lexer<Octet>
    {
        protected override IReadResult<Octet> ReadImpl(ITextScanner scanner, ITextContext context)
        {
            var read = scanner.Read();
            if (read == -1)
            {
                return ReadResult<Octet>.None;
            }
            return ReadResult<Octet>.Success(new Octet(read, context));
        }
    }
}
