using System;
using JetBrains.Annotations;

namespace Txt.Core
{
    public sealed class SyntaxError : ITextContext
    {
        public SyntaxError([NotNull] ITextContext context, string message)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            Offset = context.Offset;
            Line = context.Line;
            Column = context.Column;
            Message = message;
        }

        public int Column { get; }

        public int Line { get; }

        public string Message { get; }

        public long Offset { get; }
    }
}
