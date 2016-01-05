namespace TextFx
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    public class StringTextSource : TextSource
    {
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly Queue<char> s;

        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly Stack<char> pusback;

        public StringTextSource(string s)
        {
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s));
            }

            this.s = new Queue<char>(s.Length);
            // ReSharper disable once ForCanBeConvertedToForeach
            for (int index = 0; index < s.Length; index++)
            {
                this.s.Enqueue(s[index]);
            }

            this.pusback = new Stack<char>();
        }

        public override int Read()
        {
            if (this.pusback.Count != 0)
            {
                return this.pusback.Pop();
            }

            if (this.s.Count == 0)
            {
                return -1;
            }

            return this.s.Dequeue();
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
                if (this.pusback.Count == 0)
                {
                    if (this.s.Count == 0)
                    {
                        break;
                    }

                    buffer[offset] = this.s.Dequeue();
                }
                else
                {
                    buffer[offset] = this.pusback.Pop();
                }
            }

            return length;
        }

        public override void Unread(char c)
        {
            this.pusback.Push(c);
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

            for (int i = offset + count - 1; i >= 0; i--)
            {
                this.pusback.Push(buffer[i]);
            }
        }
    }
}