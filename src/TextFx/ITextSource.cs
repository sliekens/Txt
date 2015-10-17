namespace TextFx
{
    using System;
    using System.Text;

    /// <summary>
    ///     Provides the interface for data sources that contain text.
    /// </summary>
    public interface ITextSource : IDisposable
    {
        int Read();

        void Unread(char c);

        int Read(char[] buffer, int offset, int count);

        void Unread(char[] buffer, int offset, int count);

        Encoding Encoding { get; }
    }
}