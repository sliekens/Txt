using Txt.ABNF.Core.ALPHA;
using Txt.ABNF.Core.BIT;
using Txt.ABNF.Core.CHAR;
using Txt.ABNF.Core.CR;
using Txt.ABNF.Core.CRLF;
using Txt.ABNF.Core.CTL;
using Txt.ABNF.Core.DIGIT;
using Txt.ABNF.Core.DQUOTE;
using Txt.ABNF.Core.HEXDIG;
using Txt.ABNF.Core.HTAB;
using Txt.ABNF.Core.LF;
using Txt.ABNF.Core.LWSP;
using Txt.ABNF.Core.OCTET;
using Txt.ABNF.Core.SP;
using Txt.ABNF.Core.VCHAR;
using Txt.ABNF.Core.WSP;

namespace Txt.ABNF
{
    public class CoreGrammar : Grammar
    {
        public CoreGrammar()
        {
            AddRule("ALPHA", new AlphaLexer(this));
            AddRule("BIT", new BitLexer(this));
            AddRule("CHAR", new CharacterLexer(this));
            AddRule("CR", new CarriageReturnLexer(this));
            AddRule("CRLF", new NewLineLexer(this));
            AddRule("CTL", new ControlCharacterLexer(this));
            AddRule("DIGIT", new DigitLexer(this));
            AddRule("DQUOTE", new DoubleQuoteLexer(this));
            AddRule("HEXDIG", new HexadecimalDigitLexer(this));
            AddRule("HTAB", new HorizontalTabLexer(this));
            AddRule("LF", new LineFeedLexer(this));
            AddRule("LWSP", new LinearWhiteSpaceLexer(this));
            AddRule("OCTET", new OctetLexer(this));
            AddRule("SP", new SpaceLexer(this));
            AddRule("VCHAR", new VisibleCharacterLexer(this));
            AddRule("WSP", new WhiteSpaceLexer(this));
        }
    }
}
