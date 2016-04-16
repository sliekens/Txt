using System;
using System.Threading.Tasks;
using Jetbrains.Annotations;

namespace Txt
{
    /// <summary>
    ///     Provides the interface for data sources that contain text.
    /// </summary>
    public interface ITextSource : IDisposable
    {
        event EventHandler<PositionChangedEventArgs> OnPositionChanged;

        int Read();

        int Read([NotNull] char[] buffer, int offset, int count);

        Task<int> ReadAsync([NotNull] char[] buffer, int offset, int count);

        int ReadBlock([NotNull] char[] buffer, int offset, int count);

        Task<int> ReadBlockAsync([NotNull] char[] buffer, int offset, int count);

        void Unread(char c);

        void Unread([NotNull] char[] buffer, int offset, int count);

        Task UnreadAsync([NotNull] char[] buffer, int offset, int count);
    }
}
