using N3DSReader.Util;
using System.Collections.Generic;

namespace N3DSReader.Structures.RomFS
{
    class FileMetadataTable
    {
        public List<FileMetadata> Files = new List<FileMetadata>();

        public FileMetadataTable(byte[] bytes)
        {
            int cursor = 0;
            while (cursor != bytes.Length)
            {
                int nameLength = Hex.ParseInt(bytes, cursor + 28, 4);
                int length = 32 + nameLength + nameLength % 4;
                Files.Add(new FileMetadata(bytes[cursor..(cursor + length)]));
                cursor += length;
            }
        }

        public override string ToString()
        {
            return string.Join("\n", Files);
        }
    }
}
