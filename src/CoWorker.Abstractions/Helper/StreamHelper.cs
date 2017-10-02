

namespace System.IO
{
    using System.Threading.Tasks;
    public static class StreamHelper
    {
        public static Task<string> ReadStringAsync(this Stream stream)
            => new StreamReader(stream).ReadToEndAsync();

        public static Task<string> WriteStringAsync(this Stream stream)
            => new StreamReader(stream).ReadToEndAsync();
    }
}
