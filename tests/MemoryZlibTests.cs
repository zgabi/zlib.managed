namespace zlib.managed.Tests
{
    using System.IO;
    using System.Linq;
    using Elskom.Generic.Libs;

    public class MemoryZlibTests
    {
        private static class OldAdler32
        {
            internal static long Calculate(long adler, byte[]? buf, int index, int len)
            {
                if (buf is null)
                {
                    return 1L;
                }

                var s1 = adler & 0xffff;
                var s2 = (adler >> 16) & 0xffff;
                while (len > 0)
                {
                    // 5552 is the largest n such that 255n(n+1)/2 + (n+1)(BASE-1) <= 2^32-1
                    var k = len < 5552 ? len : 5552;
                    len -= k;
                    while (k >= 16)
                    {
                        for (var i = 0; i < 16; i++)
                        {
                            s1 += buf[index++] & 0xff;
                            s2 += s1;
                        }

                        k -= 16;
                    }

                    if (k != 0)
                    {
                        do
                        {
                            s1 += buf[index++] & 0xff;
                            s2 += s1;
                        }
                        while (--k != 0);
                    }

                    // largest prime smaller than 65536.
                    s1 %= 65521;
                    s2 %= 65521;
                }

                return (s2 << 16) | s1;
            }
        }

        [SkippableFact]
        public void TestCompress()
        {
            var decompressedData = File.ReadAllBytes("../../../els_kom.png");
            var compressedDefaultStr = MemoryZlib.Compress("../../../els_kom.png");
            var compressedDefaultByte = MemoryZlib.Compress(decompressedData);
            var compressedNoCompressionStr = MemoryZlib.Compress("../../../els_kom.png", ZlibCompression.NoCompression);
            var compressedNoCompressionByte = MemoryZlib.Compress(decompressedData, ZlibCompression.NoCompression);
            var compressedBestSpeedStr = MemoryZlib.Compress("../../../els_kom.png", ZlibCompression.BestSpeed);
            var compressedBestSpeedByte = MemoryZlib.Compress(decompressedData, ZlibCompression.BestSpeed);
            var compressedLevel2Str = MemoryZlib.Compress("../../../els_kom.png", ZlibCompression.Level2);
            var compressedLevel2Byte = MemoryZlib.Compress(decompressedData, ZlibCompression.Level2);
            var compressedLevel3Str = MemoryZlib.Compress("../../../els_kom.png", ZlibCompression.Level3);
            var compressedLevel3Byte = MemoryZlib.Compress(decompressedData, ZlibCompression.Level3);
            var compressedLevel4Str = MemoryZlib.Compress("../../../els_kom.png", ZlibCompression.Level4);
            var compressedLevel4Byte = MemoryZlib.Compress(decompressedData, ZlibCompression.Level4);
            var compressedLevel5Str = MemoryZlib.Compress("../../../els_kom.png", ZlibCompression.Level5);
            var compressedLevel5Byte = MemoryZlib.Compress(decompressedData, ZlibCompression.Level5);
            var compressedLevel7Str = MemoryZlib.Compress("../../../els_kom.png", ZlibCompression.Level7);
            var compressedLevel7Byte = MemoryZlib.Compress(decompressedData, ZlibCompression.Level7);
            var compressedLevel8Str = MemoryZlib.Compress("../../../els_kom.png", ZlibCompression.Level8);
            var compressedLevel8Byte = MemoryZlib.Compress(decompressedData, ZlibCompression.Level8);
            var compressedBestStr = MemoryZlib.Compress("../../../els_kom.png", ZlibCompression.BestCompression);
            var compressedBestByte = MemoryZlib.Compress(decompressedData, ZlibCompression.BestCompression);
            var originaldefaultcompressed = File.ReadAllBytes("../../../compressed/els_kom.png.default");
            var originalnocompression = File.ReadAllBytes("../../../compressed/els_kom.png.nocompression");
            var originalbestspeed = File.ReadAllBytes("../../../compressed/els_kom.png.bestspeed");
            var originalbestcompressed = File.ReadAllBytes("../../../compressed/els_kom.png.best");
            var originallevel2compressed = File.ReadAllBytes("../../../compressed/els_kom.png.level2");
            var originallevel3compressed = File.ReadAllBytes("../../../compressed/els_kom.png.level3");
            var originallevel4compressed = File.ReadAllBytes("../../../compressed/els_kom.png.level4");
            var originallevel5compressed = File.ReadAllBytes("../../../compressed/els_kom.png.level5");
            var originallevel7compressed = File.ReadAllBytes("../../../compressed/els_kom.png.level7");
            var originallevel8compressed = File.ReadAllBytes("../../../compressed/els_kom.png.level8");
            Skip.IfNot(compressedDefaultStr.Equals(compressedDefaultByte));
            Skip.IfNot(compressedNoCompressionStr.Equals(compressedNoCompressionByte));
            Skip.IfNot(compressedBestSpeedStr.Equals(compressedBestSpeedByte));
            Skip.IfNot(compressedBestStr.Equals(compressedBestByte));
            Skip.IfNot(compressedLevel2Str.Equals(compressedLevel2Byte));
            Skip.IfNot(compressedLevel3Str.Equals(compressedLevel3Byte));
            Skip.IfNot(compressedLevel4Str.Equals(compressedLevel4Byte));
            Skip.IfNot(compressedLevel5Str.Equals(compressedLevel5Byte));
            Skip.IfNot(compressedLevel7Str.Equals(compressedLevel7Byte));
            Skip.IfNot(compressedLevel8Str.Equals(compressedLevel8Byte));
            Skip.IfNot(originaldefaultcompressed.Equals(compressedDefaultStr));
            Skip.IfNot(originaldefaultcompressed.Equals(compressedDefaultByte));
            Skip.IfNot(originalnocompression.Equals(compressedNoCompressionStr));
            Skip.IfNot(originalnocompression.Equals(compressedNoCompressionByte));
            Skip.IfNot(originalbestspeed.Equals(compressedBestSpeedStr));
            Skip.IfNot(originalbestspeed.Equals(compressedBestSpeedByte));
            Skip.IfNot(originalbestcompressed.Equals(compressedBestStr));
            Skip.IfNot(originalbestcompressed.Equals(compressedBestByte));
            Skip.IfNot(originallevel2compressed.Equals(compressedLevel2Str));
            Skip.IfNot(originallevel2compressed.Equals(compressedLevel2Byte));
            Skip.IfNot(originallevel3compressed.Equals(compressedLevel3Str));
            Skip.IfNot(originallevel3compressed.Equals(compressedLevel3Byte));
            Skip.IfNot(originallevel4compressed.Equals(compressedLevel4Str));
            Skip.IfNot(originallevel4compressed.Equals(compressedLevel4Byte));
            Skip.IfNot(originallevel5compressed.Equals(compressedLevel5Str));
            Skip.IfNot(originallevel5compressed.Equals(compressedLevel5Byte));
            Skip.IfNot(originallevel7compressed.Equals(compressedLevel7Str));
            Skip.IfNot(originallevel7compressed.Equals(compressedLevel7Byte));
            Skip.IfNot(originallevel8compressed.Equals(compressedLevel8Str));
            Skip.IfNot(originallevel8compressed.Equals(compressedLevel8Byte));
        }

        [SkippableFact]
        public void TestCompressHash()
        {
            var compressedDefaultStr = MemoryZlib.CompressHash("../../../els_kom.png");
            var compressedDefaultBytes = MemoryZlib.CompressHash(File.ReadAllBytes("../../../els_kom.png"));
            Skip.IfNot(Enumerable.SequenceEqual(compressedDefaultStr.OutData, compressedDefaultBytes.OutData));
            Skip.IfNot(compressedDefaultStr.Adler32.Equals(compressedDefaultBytes.Adler32));
        }

        [SkippableFact(typeof(NotUnpackableException))]
        public void TestDecompress()
            => Skip.IfNot(MemoryZlib.Decompress("../../../compressed/els_kom.png.best").Equals(File.ReadAllBytes("../../../els_kom.png")));

        [SkippableFact]
        public void TestIsCompressedByZlib()
        {
            Skip.IfNot(!MemoryZlib.IsCompressedByZlib("../../../els_kom.png"));
            using var fs = File.OpenRead("../../../els_kom.png");
            Skip.IfNot(!MemoryZlib.IsCompressedByZlib(fs));
            Skip.IfNot(MemoryZlib.IsCompressedByZlib("../../../compressed/els_kom.png.best"));
            using var fs2 = File.OpenRead("../../../compressed/els_kom.png.best");
            Skip.IfNot(MemoryZlib.IsCompressedByZlib(fs2));
        }

        [SkippableFact]
        public void TestZlibVersion()
            => Skip.IfNot(!string.IsNullOrEmpty(MemoryZlib.ZlibVersion()));

        [SkippableFact]
        public void TestZlibGetAdler32()
        {
            var filedata = File.ReadAllBytes("../../../els_kom.png");
            Skip.IfNot(((uint)(OldAdler32.Calculate(OldAdler32.Calculate(0L, null, 0, 0), filedata, 0, filedata.Length) & 0xffff)).Equals(MemoryZlib.ZlibGetAdler32(filedata, 0, filedata.Length)));
        }
    }
}
