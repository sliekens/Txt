﻿namespace TextFx
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using JetBrains.Annotations;

    /// <summary>
    ///     Provides methods for reading a terminal value using the specified casing rules.
    /// </summary>
    public class TerminalLexer : Lexer<Terminal>
    {
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly IEqualityComparer<string> comparer;

        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly string terminal;

        public TerminalLexer([NotNull] string terminal, [NotNull] IEqualityComparer<string> comparer)
        {
            if (terminal == null)
            {
                throw new ArgumentNullException(nameof(terminal));
            }
            if (comparer == null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }
            this.terminal = terminal;
            this.comparer = comparer;
        }

        public override ReadResult<Terminal> Read(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var context = scanner.GetContext();
            if (scanner.EndOfInput)
            {
                return ReadResult<Terminal>.FromError(
                    new SyntaxError
                    {
                        Message = $"Unexpected end of input. Expected symbol: '{terminal}'",
                        Context = context
                    });
            }
            var result = scanner.TryMatch(terminal, comparer);
            if (!result.Success)
            {
                return ReadResult<Terminal>.FromError(
                    new SyntaxError
                    {
                        Message = $"Unexpected symbol: '{result.Text}'. Expected symbol: '{terminal}'",
                        Context = context
                    });
            }
            var element = new Terminal(result.Text, context);
            return ReadResult<Terminal>.FromResult(element);
        }
    }
}
