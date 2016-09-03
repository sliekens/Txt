using System;
using JetBrains.Annotations;

namespace Txt.Core
{
    public sealed class SyntaxError
    {
        public SyntaxError(string message, [NotNull] ITextContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            Message = message;
            Context = context;
        }

        public ITextContext Context { get; }

        public string Message { get; }
    }
}
