using N3DSReader.Util;

namespace N3DSReader.Structures.RomFS
{
    class DirectoryMetadata
    {
        public long ParentDirectoryOffset { get; }        // 0x00, 0x4
        public long SiblingDirectoryOffset { get; }       // 0x04, 0x4
        public long FirstChildDirectoryOffset { get; }    // 0x08, 0x4
        public int FirstFileOffset { get; }              // 0x0c, 0x4
        public int HTBNextCousinDirectoryOffset { get; } // 0x10, 0x4
        public int NameLength { get; }                    // 0x14, 0x4
        public string Name { get; }                       // 0x18, variable
        public long BaseOffset { get; set; }
        public long Offset { get; set; }

        public DirectoryMetadata(byte[] bytes, long offset, int cursor)
        {
            BaseOffset = offset + cursor;
            Offset = offset;
            ParentDirectoryOffset = Hex.ParseInt(bytes, 0x00, 0x4) + Offset;
            SiblingDirectoryOffset = Hex.ParseInt(bytes, 0x04, 0x4) + Offset;
            FirstChildDirectoryOffset = Hex.ParseInt(bytes, 0x08, 0x4) + Offset;
            FirstFileOffset = Hex.ParseInt(bytes, 0x0c, 0x4);
            HTBNextCousinDirectoryOffset = Hex.ParseInt(bytes, 0x10, 0x4);
            NameLength = Hex.ParseInt(bytes, 0x14, 0x4);
            Name = Hex.ToUnicode(bytes, 0x18, NameLength);
        }

        public string Details(int padding)
        {
            return $"{$"{Name}/".PadRight(padding + Name.Length / 2)} // Offset: 0x{BaseOffset:x8}, Parent Directory: 0x{ParentDirectoryOffset:x8}, First File: 0x{FirstFileOffset:x8}";
        }

        public override string ToString() => Details(16);
    }
}
