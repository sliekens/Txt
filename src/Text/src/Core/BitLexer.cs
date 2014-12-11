using System.Linq;

namespace Text.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class BitLexer : Lexer<BitToken>
    {
        public BitLexer(ITextScanner scanner)
            : base(scanner)
        {
        }

        public override BitToken Read()
        {
            var context = this.Scanner.GetContext();
            BitToken token;
            if (this.TryRead(out token))
            {
                return token;
            }

            throw new SyntaxErrorException("Expected BIT", context);
        }

        public override bool TryRead(out BitToken token)
        {
            token = default(BitToken);
            var context = this.Scanner.GetContext();
            foreach (var character in new[] { '0', '1' }.Where(character => this.Scanner.TryMatch(character)))
            {
                token = new BitToken
                {
                    Line = context.Line,
                    Column = context.Column,
                    Offset = context.Offset,
                    Data = char.ToString(character)
                };

                return true;
            }

            return false;
        }
    }
}
