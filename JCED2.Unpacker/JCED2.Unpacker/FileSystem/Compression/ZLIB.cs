﻿using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;

namespace JCED2.Unpacker
{
    class ZLIB
    {
        public static Byte[] iDecompress(Byte[] lpBuffer)
        {
            var TOutMemoryStream = new MemoryStream();
            using (MemoryStream TMemoryStream = new MemoryStream(lpBuffer) { Position = 2 })
            {
                using (DeflateStream TDeflateStream = new DeflateStream(TMemoryStream, CompressionMode.Decompress, false))
                {
                    TDeflateStream.CopyTo(TOutMemoryStream);
                    TDeflateStream.Dispose();
                }
                TMemoryStream.Dispose();
            }

            return TOutMemoryStream.ToArray();
        }
    }
}