using Txt.Core;

namespace Txt.ABNF.Core.CHAR
{
    public class CharacterParserFactory : ParserFactory<Character, char>
    {
        public static CharacterParserFactory Default { get; } = new CharacterParserFactory();

        public override IParser<Character, char> Create()
        {
            return new CharacterParser();
        }
    }
}
