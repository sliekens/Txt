using System;
using System.Collections.Generic;
using System.Reflection;

namespace Txt.ABNF
{
    public sealed class AbnfRegistrations : Registrations
    {
        public static IEnumerable<Registration> GetRegistrations(GetInstanceDelegate getInstance)
        {
            if (getInstance == null)
            {
                throw new ArgumentNullException(nameof(getInstance));
            }
            yield return new Registration(typeof(ITerminalLexerFactory), typeof(TerminalLexerFactory));
            yield return new Registration(typeof(IValueRangeLexerFactory), typeof(ValueRangeLexerFactory));
            yield return new Registration(typeof(IAlternationLexerFactory), typeof(AlternationLexerFactory));
            yield return new Registration(typeof(IGreedyAlternationLexerFactory), typeof(GreedyAlternationLexerFactory));
            yield return new Registration(typeof(IConcatenationLexerFactory), typeof(ConcatenationLexerFactory));
            yield return new Registration(typeof(IRepetitionLexerFactory), typeof(RepetitionLexerFactory));
            yield return new Registration(typeof(IOptionLexerFactory), typeof(OptionLexerFactory));
            foreach (var registration in GetRegistrations(typeof(AbnfRegistrations).GetTypeInfo().Assembly, getInstance))
            {
                yield return registration;
            }
        }
    }
}
