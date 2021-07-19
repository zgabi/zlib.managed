// Copyright (c) 2018-2021, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: see LICENSE for more details.

namespace Elskom.Generic.Libs
{
    using System;

    internal static class Adler32
    {
        internal static long Calculate(long adler, ReadOnlySpan<byte> buf)
        {
            if (buf.Length is 0)
            {
                return 1L;
            }

            var index = 0;
            var len = buf.Length;
            var s1 = adler & 0xffff;
            var s2 = (adler >> 16) & 0xffff;
            while (len > 0)
            {
                // 5552 is the largest n such that 255n(n+1)/2 + (n+1)(BASE-1) <= 2^32-1
                var k = len < 5552 ? len : 5552;
                len -= k;
                for (var i = k; i > 0; i--)
                {
                    s1 += buf[index++] & 0xff;
                    s2 += s1;
                    k--;
                }

                // largest prime smaller than 65536.
                s1 %= 65521;
                s2 %= 65521;
            }

            return (s2 << 16) | s1;
        }
    }
}
