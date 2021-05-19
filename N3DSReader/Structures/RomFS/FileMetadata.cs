using N3DSReader.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace N3DSReader.Structures.RomFS
{
    class FileMetadata
    {
        public int ContainingDirectoryOffset { get; } // 0x00, 0x4
        public int NextSiblingFileOffset { get; }     // 0x04, 0x4
        public long FileDataOffset { get; }            // 0x08, 0x8
        public long FileDataLength { get; }            // 0x10, 0x8
        public int HTBNextCousinFileOffset { get; }   // 0x18, 0x4
        public int NameLength { get; }                // 0x1c, 0x4
        public string Name { get; }                   // 0x20, variable
        public long BaseOffset { get; set; }
        public long Offset { get; set; }

        public FileMetadata(byte[] bytes, long offset, int cursor)
        {
            BaseOffset = offset + cursor;
            Offset = offset;
            ContainingDirectoryOffset = Hex.ParseInt(bytes, 0x00, 0x4);
            NextSiblingFileOffset = Hex.ParseInt(bytes, 0x04, 0x4);
            FileDataOffset = Hex.ParseInt(bytes, 0x08, 0x4) + Offset;
            FileDataLength = Hex.ParseInt(bytes, 0x10, 0x8);
            HTBNextCousinFileOffset = Hex.ParseInt(bytes, 0x18, 0x4);
            NameLength = Hex.ParseInt(bytes, 0x1c, 0x4);
            Name = Hex.ToUnicode(bytes, 0x20, NameLength);
        }

        public string Details(int padding)
        {
            return $"{Name.PadRight(padding + Name.Length / 2)} // Directory: 0x{ContainingDirectoryOffset:x8}, Offset: 0x{BaseOffset:x8}, Data Offset: 0x{FileDataOffset:x8}, Size: {IO.FormatSize(FileDataLength)}";
        }

        public override string ToString() => Details(32);
    }
}
