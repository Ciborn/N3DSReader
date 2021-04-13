using N3DSReader.Util;

namespace N3DSReader.Structures.ExeFS
{
    class ExeFSFileHeader
    {
        public string Name { get; } // 0x0, 0x8
        public int Offset { get; }  // 0x8, 0x4
        public int Size { get; }    // 0xc, 0x4

        public ExeFSFileHeader(byte[] bytes)
        {
            Name = Hex.ToString(bytes, 0x0, 0x8);
            Offset = Hex.ParseInt(bytes, 0x8, 0x4);
            Size = Hex.ParseInt(bytes, 0xc, 0x4);
        }

        public override string ToString()
        {
            return $"{Name, -8} // Offset: 0x{Offset:x8}, Size: {Size / 1024} KB";
        }
    }
}
