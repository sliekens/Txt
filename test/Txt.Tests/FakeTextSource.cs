using System;
using System.Text;
using System.Threading.Tasks;

namespace Txt
{
    public class FakeTextSource : ITextSource
    {
        public Encoding Encoding
        {
            get
            {
                if (this.OnEncodingGet == null)
                {
                    throw new NotImplementedException();
                }

                return this.OnEncodingGet();
            }
        }

        public Action OnDispose { get; set; }

        public Func<Encoding> OnEncodingGet { get; set; }

        public Func<int> OnRead { get; set; }

        public Func<char[], int, int, Task<int>> OnReadAsyncCharArrayInt32Int32 { get; set; }

        public Func<char[], int, int, Task<int>> OnReadBlockAsyncCharArrayInt32Int32 { get; set; }

        public Func<char[], int, int, int> OnReadBlockCharArrayInt32Int32 { get; set; }

        public Func<char[], int, int, int> OnReadCharArrayInt32Int32 { get; set; }

        public Action<char> OnUnreadChar { get; set; }

        public Action<char[], int, int> OnUnreadCharArrayInt32Int32 { get; set; }

        public Func<char[], int, int, Task> OnUnreadCharAsyncArrayInt32Int32 { get; set; }

        public void Dispose()
        {
            if (this.OnDispose == null)
            {
                throw new NotImplementedException();
            }

            this.OnDispose();
        }

        public int Read()
        {
            if (this.OnRead == null)
            {
                throw new NotImplementedException();
            }

            return this.OnRead();
        }

        public int Read(char[] buffer, int offset, int count)
        {
            if (this.OnReadCharArrayInt32Int32 == null)
            {
                throw new NotImplementedException();
            }

            return this.OnReadCharArrayInt32Int32(buffer, offset, count);
        }

        public Task<int> ReadAsync(char[] buffer, int offset, int count)
        {
            if (this.OnReadAsyncCharArrayInt32Int32 == null)
            {
                throw new NotImplementedException();
            }

            return this.OnReadAsyncCharArrayInt32Int32(buffer, offset, count);
        }

        public int ReadBlock(char[] buffer, int offset, int count)
        {
            if (this.OnReadBlockCharArrayInt32Int32 == null)
            {
                throw new NotImplementedException();
            }

            return this.OnReadBlockCharArrayInt32Int32(buffer, offset, count);
        }

        public Task<int> ReadBlockAsync(char[] buffer, int offset, int count)
        {
            if (this.OnReadBlockAsyncCharArrayInt32Int32 == null)
            {
                throw new NotImplementedException();
            }

            return this.OnReadBlockAsyncCharArrayInt32Int32(buffer, offset, count);
        }

        public void Unread(char c)
        {
            if (this.OnUnreadChar == null)
            {
                throw new NotImplementedException();
            }

            this.OnUnreadChar(c);
        }

        public void Unread(char[] buffer, int offset, int count)
        {
            if (this.OnUnreadCharArrayInt32Int32 == null)
            {
                throw new NotImplementedException();
            }

            this.OnUnreadCharArrayInt32Int32(buffer, offset, count);
        }

        public Task UnreadAsync(char[] buffer, int offset, int count)
        {
            if (this.OnUnreadCharAsyncArrayInt32Int32 == null)
            {
                throw new NotImplementedException();
            }

            return this.OnUnreadCharAsyncArrayInt32Int32(buffer, offset, count);
        }
    }
}