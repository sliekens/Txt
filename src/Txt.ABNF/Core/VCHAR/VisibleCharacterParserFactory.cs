using Txt.Core;

namespace Txt.ABNF.Core.VCHAR
{
    public class VisibleCharacterParserFactory : ParserFactory<VisibleCharacter, char>
    {
        public static VisibleCharacterParserFactory Default { get; } = new VisibleCharacterParserFactory();

        public override IParser<VisibleCharacter, char> Create()
        {
            return new VisibleCharacterParser();
        }
    }
}
