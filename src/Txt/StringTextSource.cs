using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Txt
{
    public class StringTextSource : TextSource
    {
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly Stack<char> pushback;

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
            pushback = new Stack<char>();
        }

        protected override int PeekImpl()
        {
            if (pushback.Count != 0)
            {
                return pushback.Peek();
            }
            if (s.Count != 0)
            {
                return s.Peek();
            }
            return -1;
        }

        public override int Read()
        {
            if (pushback.Count != 0)
            {
                return pushback.Pop();
            }
            if (s.Count != 0)
            {
                return s.Dequeue();
            }
            return -1;
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
                if (pushback.Count == 0)
                {
                    if (s.Count == 0)
                    {
                        break;
                    }
                    buffer[offset] = s.Dequeue();
                }
                else
                {
                    buffer[offset] = pushback.Pop();
                }
            }
            return length;
        }

        public override void Unread(char c)
        {
            pushback.Push(c);
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
                pushback.Push(buffer[i]);
            }
        }
    }
}
