namespace TextFx
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    ///     Provides the interface for data sources that contain text.
    /// </summary>
    public interface ITextSource : IDisposable
    {
        int Read();

        void Unread(char c);

        int Read(char[] buffer, int offset, int count);

        Task<int> ReadAsync (char[] buffer, int offset, int count);

        int ReadBlock(char[] buffer, int offset, int count);

        Task<int> ReadBlockAsync(char[] buffer, int offset, int count);

        void Unread(char[] buffer, int offset, int count);

        Task UnreadAsync(char[] buffer, int offset, int count);
    }
}