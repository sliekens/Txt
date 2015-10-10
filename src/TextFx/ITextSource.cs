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

        int Read(char[] buffer, int index, int count);

        void Unread(char[] buffer, int index, int count);

        Encoding Encoding { get; }
    }
}