using N3DSReader.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace N3DSReader.Structures.RomFS
{
    class DirectoryMetadataTable
    {
        public Dictionary<long, DirectoryMetadata> Directories { get; } = new Dictionary<long, DirectoryMetadata>();
        public long Offset { get; set; }

        public DirectoryMetadataTable(byte[] bytes, long offset)
        {
            Offset = offset;
            int cursor = 24;
            while (cursor != bytes.Length)
            {
                int nameLength = Hex.ParseInt(bytes, cursor + 20, 4);
                int length = 24 + nameLength + nameLength % 4;
                Directories.Add(Offset + cursor, new DirectoryMetadata(bytes[cursor..(cursor + length)], Offset, cursor));
                cursor += length;
            }
        }

        public override string ToString()
        {
            return string.Join("\n", Directories.Values);
        }
    }
}
