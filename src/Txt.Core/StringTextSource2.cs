using System;
using JetBrains.Annotations;
using Txt.Core;

namespace Txt.Core
{
    public class StringTextSource2 : TextSource2
    {
        public StringTextSource2([NotNull] string data)
            : base(new Func<char[]>(
                () =>
                {
                    if (data == null)
                    {
                        throw new ArgumentNullException(nameof(data));
                    }
                    return data.ToCharArray();
                })())
        {
        }

        protected override int ReadImpl(char[] buffer, int offset, int count)
        {
            return 0;
        }
    }

    //public class StringTextSource2 : ITextSource2
    //{
    //    /// <summary>A character array that holds 0 or more characters to be read.</summary>
    //    private readonly char[] data;

    //    /// <summary>
    //    ///     The number of characters in the <see cref="data" /> to be read. This is not the same as the capacity of the
    //    ///     array.
    //    /// </summary>
    //    private readonly int dataLength;

    //    private readonly TextReader s = new StringReader("");

    //    /// <summary>The index of the next character to be read from the <see cref="data" />.</summary>
    //    private int dataIndex;

    //    public StringTextSource2([NotNull] string data)
    //    {
    //        if (data == null)
    //        {
    //            throw new ArgumentNullException(nameof(data));
    //        }
    //        this.data = data.ToCharArray();
    //        dataLength = data.Length;
    //    }

    //    public long Offset => dataIndex;

    //    public void Dispose()
    //    {
    //    }

    //    public int Peek()
    //    {
    //        if (dataIndex < dataLength)
    //        {
    //            return data[dataIndex];
    //        }
    //        return -1;
    //    }

    //    public int Read()
    //    {
    //        if (dataIndex >= dataLength)
    //        {
    //            return -1;
    //        }
    //        return data[dataIndex++];
    //    }

    //    public int Read(char[] buffer, int offset, int count)
    //    {
    //        if (buffer == null)
    //        {
    //            throw new ArgumentNullException(nameof(buffer));
    //        }
    //        if (offset < 0)
    //        {
    //            throw new ArgumentOutOfRangeException(nameof(offset), "Precondition: offset >= 0");
    //        }
    //        if (count < 0)
    //        {
    //            throw new ArgumentOutOfRangeException(nameof(count), "Precondition: count >= 0");
    //        }
    //        if (offset + count > buffer.Length)
    //        {
    //            throw new ArgumentOutOfRangeException(nameof(count), "Precondition: offset + count <= buffer.Length");
    //        }
    //        if (count == 0)
    //        {
    //            return 0;
    //        }
    //        if (count > dataLength - dataIndex)
    //        {
    //            count = dataLength - dataIndex;
    //        }
    //        Array.Copy(data, dataIndex, buffer, offset, count);
    //        dataIndex += count;
    //        return count;
    //    }

    //    public void Seek(long offset)
    //    {
    //        if ((offset == -1) || (offset > dataLength))
    //        {
    //            throw new ArgumentOutOfRangeException(nameof(offset));
    //        }
    //        dataIndex = (int)offset;
    //    }
    //}
}
