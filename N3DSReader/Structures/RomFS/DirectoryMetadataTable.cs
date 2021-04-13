using N3DSReader.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace N3DSReader.Structures.RomFS
{
    class DirectoryMetadataTable
    {
        public List<DirectoryMetadata> Directories { get; } = new List<DirectoryMetadata>();

        public DirectoryMetadataTable(byte[] bytes)
        {
            int cursor = 24;
            while (cursor != bytes.Length)
            {
                int nameLength = Hex.ParseInt(bytes, cursor + 20, 4);
                int length = 24 + nameLength + nameLength % 4;
                Directories.Add(new DirectoryMetadata(bytes[cursor..(cursor + length)]));
                cursor += length;
            }
        }

        public override string ToString()
        {
            return string.Join("\n", Directories);
        }
    }
}
