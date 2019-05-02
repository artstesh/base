using artstesh.data.Helpers;
using artstesh.tests.FakeFactories;
using Xunit;

namespace artstesh.tests.Data
{
    public class StringCompressorTests
    {
        [Theory]
        [AutoMoqData]
        public void Compress_Success(string text)
        {
            var compressed = StringCompressor.CompressString(text);
            var decompressed = StringCompressor.DecompressString(compressed);
            //
            Assert.True(text == decompressed);
        }

        [Theory]
        [AutoMoqData]
        public void Compress_Compressed(string text)
        {
            var compressed = StringCompressor.CompressString(text);
            //
            Assert.True(text.Length < compressed.Length);
        }
    }
}