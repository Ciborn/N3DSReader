using N3DSReader.Util;
using System;

namespace N3DSReader.Structures.RomFS
{
    class L3Header
    {
        public int Length { get; }                       // 0x00, 0x4
        public int DirectoryHashTableOffset { get; }     // 0x04, 0x4
        public int DirectoryHashTableLength { get; }     // 0x08, 0x4
        public int DirectoryMetadataTableOffset { get; } // 0x0c, 0x4
        public int DirectoryMetadataTableLength { get; } // 0x10, 0x4
        public int FileHashTableOffset { get; }          // 0x14, 0x4
        public int FileHashTableLength { get; }          // 0x18, 0x4
        public int FileMetadataTableOffset { get; }      // 0x1c, 0x4
        public int FileMetadataTableLength { get; }      // 0x20, 0x4
        public int FileDataOffset { get; }               // 0x24, 0x4

        public L3Header(byte[] bytes)
        {
            if (bytes.Length != 40) throw new ArgumentException("There should be 40 bytes!");
            Length = Hex.ParseInt(bytes, 0x00, 0x4);
            DirectoryHashTableOffset = Hex.ParseInt(bytes, 0x04, 0x4);
            DirectoryHashTableLength = Hex.ParseInt(bytes, 0x08, 0x4);
            DirectoryMetadataTableOffset = Hex.ParseInt(bytes, 0x0c, 0x4);
            DirectoryMetadataTableLength = Hex.ParseInt(bytes, 0x10, 0x4);
            FileHashTableOffset = Hex.ParseInt(bytes, 0x14, 0x4);
            FileHashTableLength = Hex.ParseInt(bytes, 0x18, 0x4);
            FileMetadataTableOffset = Hex.ParseInt(bytes, 0x1c, 0x4);
            FileMetadataTableLength = Hex.ParseInt(bytes, 0x20, 0x4);
            FileDataOffset = Hex.ParseInt(bytes, 0x24, 0x4);
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine,
                $"Length                         : {Length}",
                $"Directory Hash Table Offset    : 0x{DirectoryHashTableOffset:x8}",
                $"Directory Hash Table Length    : {DirectoryHashTableLength}",
                $"Directory Metadata Table Offset: 0x{DirectoryMetadataTableOffset:x8}",
                $"Directory Metadata Table Length: {DirectoryMetadataTableLength}",
                $"File Hash Table Offset         : 0x{FileHashTableOffset:x8}",
                $"File Hash Table Length         : {FileHashTableLength}",
                $"File Metadata Table Offset     : 0x{FileMetadataTableOffset:x8}",
                $"File Metadata Table Length     : {FileMetadataTableLength}",
                $"File Data Offset               : 0x{FileDataOffset:x8}"
            );
        }
    }
}
