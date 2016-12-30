using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Txt.ABNF.Core.CTL
{
    public sealed class ControlCharacterLexerFactory : RuleLexerFactory<ControlCharacter>
    {
        static ControlCharacterLexerFactory()
        {
            Default = new ControlCharacterLexerFactory();
        }

        [NotNull]
        public static ControlCharacterLexerFactory Default { get; }

        public override ILexer<ControlCharacter> Create()
        {
            var innerLexer = Alternation.Create(
                ValueRange.Create('\x00', '\x1F'),
                Terminal.Create(@"\x7F", StringComparer.Ordinal));
            return new ControlCharacterLexer(innerLexer);
        }
    }
}
