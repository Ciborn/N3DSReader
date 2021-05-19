using N3DSReader.Util;
using System.Collections.Generic;

namespace N3DSReader.Structures.RomFS
{
    class FileMetadataTable
    {
        public Dictionary<long, FileMetadata> Files = new Dictionary<long, FileMetadata>();
        public long Offset { get; set; }

        public FileMetadataTable(byte[] bytes, long offset)
        {
            Offset = offset;
            int cursor = 0;
            while (cursor != bytes.Length)
            {
                int nameLength = Hex.ParseInt(bytes, cursor + 28, 4);
                int length = 32 + nameLength + nameLength % 4;
                Files.Add(Offset + cursor, new FileMetadata(bytes[cursor..(cursor + length)], Offset,  cursor));
                cursor += length;
            }
        }

        public override string ToString()
        {
            return string.Join("\n", Files.Values);
        }
    }
}
