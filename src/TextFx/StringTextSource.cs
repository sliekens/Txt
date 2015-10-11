namespace TextFx
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class StringTextSource : ITextSource
    {
        private Queue<char> s;
        private Stack<char> pusback;

        public StringTextSource(string s)
        {
            this.s = new Queue<char>(s.Length);
            for (int index = 0; index < s.Length; index++)
            {
                this.s.Enqueue(s[index]);
            }

            this.pusback = new Stack<char>();
        }

        public Encoding Encoding { get; } = Encoding.Unicode;

        public void Dispose()
        {
        }

        public int Read()
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

        public int Read(char[] buffer, int index, int count)
        {
            int len = 0;
            for (int i = index; i < count; i++, len++)
            {
                if (this.pusback.Count == 0)
                {
                    if (this.s.Count == 0)
                    {
                        break;
                    }

                    buffer[i] = this.s.Dequeue();
                }
                else
                {
                    buffer[i] = this.pusback.Pop();
                }
            }

            return len;
        }

        public void Unread(char c)
        {
            this.pusback.Push(c);
        }

        public void Unread(char[] buffer, int index, int count)
        {
            for (int i = count - 1; i >= index; i--)
            {
                this.pusback.Push(buffer[i]);
            }
        }
    }
}