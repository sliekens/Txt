using System;
using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.CR
{
    /// <summary>Creates instances of the <see cref="CarriageReturnLexer" /> class.</summary>
    public class CarriageReturnLexerFactory : ILexerFactory<CarriageReturn>
    {
        private ILexer<CarriageReturn> instance;

        static CarriageReturnLexerFactory()
        {
            Default = new CarriageReturnLexerFactory(ABNF.TerminalLexerFactory.Default);
        }

        /// <summary>
        /// </summary>
        /// <param name="terminalLexerFactory"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public CarriageReturnLexerFactory([NotNull] ITerminalLexerFactory terminalLexerFactory)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }
            TerminalLexerFactory = terminalLexerFactory;
        }

        [NotNull]
        public static CarriageReturnLexerFactory Default { get; }

        [NotNull]
        public ITerminalLexerFactory TerminalLexerFactory { get; }

        /// <inheritdoc />
        public ILexer<CarriageReturn> Create()
        {
            var innerLexer = TerminalLexerFactory.Create("\x0D", StringComparer.Ordinal);
            return new CarriageReturnLexer(innerLexer);
        }

        [NotNull]
        public CarriageReturnLexerFactory UseTerminalLexerFactory([NotNull] ITerminalLexerFactory terminalLexerFactory)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }
            return new CarriageReturnLexerFactory(terminalLexerFactory);
        }
    }
}
