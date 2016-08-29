using System;
using JetBrains.Annotations;

namespace Txt.Core
{
    public interface ITextSource2 : IDisposable
    {
        long Offset { get; }

        int Peek();

        int Read();

        int Read([NotNull] char[] buffer, int offset, int count);

        void Seek(long offset);
    }
}
