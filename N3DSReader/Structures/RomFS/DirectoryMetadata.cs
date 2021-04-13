using N3DSReader.Util;

namespace N3DSReader.Structures.RomFS
{
    class DirectoryMetadata
    {
        public int ParentDirectoryOffset { get; }        // 0x00, 0x4
        public int SiblingDirectoryOffset { get; }       // 0x04, 0x4
        public int FirstChildDirectoryOffset { get; }    // 0x08, 0x4
        public int FirstFileOffset { get; }              // 0x0c, 0x4
        public int HTBNextCousinDirectoryOffset { get; } // 0x10, 0x4
        public int NameLength { get; }                   // 0x14, 0x4
        public string Name { get; }                      // 0x18, variable

        public DirectoryMetadata(byte[] bytes)
        {
            ParentDirectoryOffset = Hex.ParseInt(bytes, 0x00, 0x4);
            SiblingDirectoryOffset = Hex.ParseInt(bytes, 0x04, 0x4);
            FirstChildDirectoryOffset = Hex.ParseInt(bytes, 0x08, 0x4);
            FirstFileOffset = Hex.ParseInt(bytes, 0x0c, 0x4);
            HTBNextCousinDirectoryOffset = Hex.ParseInt(bytes, 0x10, 0x4);
            NameLength = Hex.ParseInt(bytes, 0x14, 0x4);
            Name = Hex.ToUnicode(bytes, 0x18, NameLength);
        }

        public override string ToString()
        {
            return $"{Name.PadRight(16 + Name.Length / 2)} // Parent Directory: 0x{ParentDirectoryOffset:x8}, First File: 0x{FirstFileOffset:x8}";
        }
    }
}
