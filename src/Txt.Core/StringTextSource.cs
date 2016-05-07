using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Txt.Core
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

        protected override int ReadImpl()
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

        protected override int ReadImpl(char[] buffer, int offset, int count)
        {
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

        protected override void UnreadImpl(char c)
        {
            pushback.Push(c);
        }

        protected override void UnreadImpl(char[] buffer, int offset, int count)
        {
            for (var i = offset + count - 1; i >= 0; i--)
            {
                pushback.Push(buffer[i]);
            }
        }
    }
}
