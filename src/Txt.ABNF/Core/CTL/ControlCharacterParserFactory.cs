using Txt.Core;

namespace Txt.ABNF.Core.CTL
{
    public class ControlCharacterParserFactory : ParserFactory<ControlCharacter, char>
    {
        public static ControlCharacterParserFactory Default { get; } = new ControlCharacterParserFactory();

        public override IParser<ControlCharacter, char> Create()
        {
            return new ControlCharacterParser();
        }
    }
}
