using System;
using JetBrains.Annotations;

namespace Txt.Core
{
    public static class LexerExtensions
    {
        public static TElement Read<TElement>([NotNull] this ILexer<TElement> instance, [NotNull] ITextScanner scanner) where TElement : Element
        {
            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var offset = scanner.Offset;
            scanner.StartRecording();
            TElement candidate = null;
            try
            {
                var context = scanner.GetContext();
                foreach (var element in instance.Read(scanner, context))
                {
                    if (candidate == null)
                    {
                        candidate = element;
                    }
                    else if (element.Text.Length > candidate.Text.Length)
                    {
                        candidate = element;
                    }
                }
                if (candidate == null)
                {
                    return null;
                }
                scanner.Seek(offset + candidate.Text.Length);
            }
            finally
            {
                scanner.StopRecording();
            }
            return candidate;
        }
    }
}
