using System;
using System.IO;
using System.Collections.Generic;

namespace JCED2.Unpacker
{
    class JfsUnpack
    {
        static List<JfsEntry> m_EntryTable = new List<JfsEntry>();

        public static void iDoIt(String m_Archive, String m_DstFolder)
        {
            JfsHashList.iLoadProject();
            using (FileStream THdrStream = File.OpenRead(m_Archive + "hdr"))
            {
                var m_Header = new JfsHeader();
                m_Header.dwTotalFiles = THdrStream.ReadInt32();

                m_EntryTable.Clear();
                for (Int32 i = 0; i < m_Header.dwTotalFiles; i++)
                {
                    UInt32 dwHash = THdrStream.ReadUInt32();
                    UInt32 dwOffset = THdrStream.ReadUInt32();
                    Int32 dwCompressedSize = THdrStream.ReadInt32();
                    Int32 dwDecompressedSize = THdrStream.ReadInt32();
                    Int32 dwCompressionLevel = THdrStream.ReadInt32();
                    Int32 dwCompressed = THdrStream.ReadInt32();

                    var TEntry = new JfsEntry
                    {
                        dwHash = dwHash,
                        dwOffset = dwOffset,
                        dwCompressedSize = dwCompressedSize,
                        dwDecompressedSize = dwDecompressedSize,
                        dwCompressionLevel = dwCompressionLevel,
                        dwCompressed = dwCompressed,
                    };

                    m_EntryTable.Add(TEntry);
                }

                THdrStream.Dispose();

                using (FileStream TJfsStream = File.OpenRead(m_Archive))
                {
                    foreach (var m_Entry in m_EntryTable)
                    {
                        String m_FileName = JfsHashList.iGetNameFromHashList(m_Entry.dwHash);
                        String m_FullPath = m_DstFolder + m_FileName;

                        Utils.iSetInfo("[UNPACKING]: " + m_FileName);
                        Utils.iCreateDirectory(m_FullPath);

                        TJfsStream.Seek(m_Entry.dwOffset, SeekOrigin.Begin);

                        if (m_Entry.dwCompressed == 0)
                        {
                            var lpBuffer = TJfsStream.ReadBytes(m_Entry.dwDecompressedSize);
                            File.WriteAllBytes(m_FullPath, lpBuffer);
                        }
                        else if (m_Entry.dwCompressed == 1)
                        {
                            var lpSrcBuffer = TJfsStream.ReadBytes(m_Entry.dwCompressedSize);
                            var lpDstBuffer = ZLIB.iDecompress(lpSrcBuffer);
                            File.WriteAllBytes(m_FullPath, lpDstBuffer);
                        }
                    }

                    TJfsStream.Dispose();
                }
            }
        }
    }
}
