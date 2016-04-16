using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Txt
{
    public class StringTextSource : TextSource
    {
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly Stack<char> pusback;

        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly Queue<char> s;

        public StringTextSource(string s)
        {
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s));
            }
            this.s = new Queue<char>(s.Length);

            // ReSharper disable once ForCanBeConvertedToForeach
            for (var index = 0; index < s.Length; index++)
            {
                this.s.Enqueue(s[index]);
            }
            pusback = new Stack<char>();
        }

        public override int Read()
        {
            if (pusback.Count != 0)
            {
                return pusback.Pop();
            }
            if (s.Count == 0)
            {
                return -1;
            }
            return s.Dequeue();
        }

        public override int Read(char[] buffer, int offset, int count)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }
            if (offset < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(offset), "Precondition: offset >= 0");
            }
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count), "Precondition: count >= 0");
            }
            if (offset + count > buffer.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(count), "Precondition: offset + count <= buffer.Length");
            }
            int length;
            for (length = 0; length < count; length++, offset++)
            {
                if (pusback.Count == 0)
                {
                    if (s.Count == 0)
                    {
                        break;
                    }
                    buffer[offset] = s.Dequeue();
                }
                else
                {
                    buffer[offset] = pusback.Pop();
                }
            }
            return length;
        }

        public override void Unread(char c)
        {
            pusback.Push(c);
        }

        public override void Unread(char[] buffer, int offset, int count)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }
            if (offset < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(offset), "Precondition: offset >= 0");
            }
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count), "Precondition: count >= 0");
            }
            if (offset + count > buffer.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(count), "Precondition: offset + count <= buffer.Length");
            }
            for (var i = offset + count - 1; i >= 0; i--)
            {
                pusback.Push(buffer[i]);
            }
        }
    }
}
