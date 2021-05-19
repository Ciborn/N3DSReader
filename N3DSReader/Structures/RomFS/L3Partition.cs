using N3DSReader.Util;
using System.IO;
using System.Linq;

namespace N3DSReader.Structures.RomFS
{
    class L3Partition
    {
        public L3Header Header { get; }
        public DirectoryMetadataTable DirectoryMetadataTable { get; }
        public FileMetadataTable FileMetadataTable { get; }
        public string PartitionStructure { get; set; }

        public L3Partition(FileStream fs, long offset)
        {
            fs.Seek(offset, SeekOrigin.Begin);
            Header = new L3Header(IO.ReadData(fs, 40), offset);

            fs.Seek(Header.DirectoryMetadataTableOffset, SeekOrigin.Begin);
            byte[] dBytes = IO.ReadData(fs, Header.DirectoryMetadataTableLength);
            DirectoryMetadataTable = new DirectoryMetadataTable(dBytes, Header.DirectoryMetadataTableOffset);

            fs.Seek(Header.FileMetadataTableOffset, SeekOrigin.Begin);
            byte[] fBytes = IO.ReadData(fs, Header.FileMetadataTableLength);
            FileMetadataTable = new FileMetadataTable(fBytes, Header.FileMetadataTableOffset);

            DirectoryMetadata firstDirectory = DirectoryMetadataTable.Directories.First().Value;
            string ExploreFolder(DirectoryMetadata directory, string final = "", int level = 0)
            {
                // final += $"{string.Concat(Enumerable.Repeat(" ", level * 2))}{directory.Name}/\n";
                final += $"\n{string.Concat(Enumerable.Repeat(" ", level * 2))}{directory.Details(42 - level * 2)}";

                long currentOffset = directory.FirstFileOffset;
                while (currentOffset % 2 == 0)
                {
                    FileMetadata file = FileMetadataTable.Files[currentOffset + FileMetadataTable.Offset];
                    // final += $"{string.Concat(Enumerable.Repeat(" ", level * 2 + 2))}{file.Name}\n";
                    final += $"\n{string.Concat(Enumerable.Repeat(" ", level * 2 + 2))}{file.Details(40 - level * 2)}";
                    currentOffset = currentOffset == file.NextSiblingFileOffset + 1 ? 0 : file.NextSiblingFileOffset;
                }

                if (directory.Offset != directory.FirstChildDirectoryOffset + 1)
                {
                    final = ExploreFolder(DirectoryMetadataTable.Directories[directory.FirstChildDirectoryOffset], final, level + 1);
                }
                if (directory.Offset != directory.SiblingDirectoryOffset + 1)
                {
                    final = ExploreFolder(DirectoryMetadataTable.Directories[directory.SiblingDirectoryOffset], final, level);
                }
                return final;
            }
            PartitionStructure = ExploreFolder(firstDirectory);
            foreach (FileMetadata file in FileMetadataTable.Files.Values.Where(f => f.ContainingDirectoryOffset == 0))
            {
                PartitionStructure += $"\n{file.Details(42)}";
            }
        }

        public override string ToString()
        {
            return PartitionStructure;
        }
    }
}
