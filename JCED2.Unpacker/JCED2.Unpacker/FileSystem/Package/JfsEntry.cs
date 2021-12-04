using System;

namespace JCED2.Unpacker
{
    class JfsEntry
    {
        public UInt32 dwHash { get; set; }
        public UInt32 dwOffset { get; set; }
        public Int32 dwCompressedSize { get; set; }
        public Int32 dwDecompressedSize { get; set; }
        public Int32 dwCompressionLevel { get; set; } // 4 or compression type
        public Int32 dwCompressed { get; set; } // 0,1 Boolean
    }
}
