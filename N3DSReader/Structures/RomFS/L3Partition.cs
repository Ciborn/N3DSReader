using N3DSReader.Util;
using System.IO;

namespace N3DSReader.Structures.RomFS
{
    class L3Partition
    {
        public L3Header Header { get; }
        public DirectoryMetadataTable DirectoryMetadataTable { get; }
        public FileMetadataTable FileMetadataTable { get; }

        public L3Partition(FileStream fs, int offset)
        {
            fs.Seek(offset, SeekOrigin.Begin);
            Header = new L3Header(IO.ReadData(fs, 40));

            fs.Seek(offset + Header.DirectoryMetadataTableOffset, SeekOrigin.Begin);
            byte[] dBytes = IO.ReadData(fs, Header.DirectoryMetadataTableLength);
            DirectoryMetadataTable = new DirectoryMetadataTable(dBytes);

            fs.Seek(offset + Header.FileMetadataTableOffset, SeekOrigin.Begin);
            byte[] fBytes = IO.ReadData(fs, Header.FileMetadataTableLength);
            FileMetadataTable = new FileMetadataTable(fBytes);
        }
    }
}
