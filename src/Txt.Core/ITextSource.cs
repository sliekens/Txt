using System;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Txt.Core
{
    /// <summary>
    ///     Provides the interface for data sources that contain text.
    /// </summary>
    public interface ITextSource : IDisposable
    {
        int Peek();

        int Read();

        int Read([NotNull] char[] buffer, int offset, int count);

        Task<int> ReadAsync([NotNull] char[] buffer, int offset, int count);

        int ReadBlock([NotNull] char[] buffer, int offset, int count);

        Task<int> ReadBlockAsync([NotNull] char[] buffer, int offset, int count);

        void Unread(char c);

        void Unread([NotNull] char[] buffer, int offset, int count);
    }
}
