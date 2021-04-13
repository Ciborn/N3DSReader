using N3DSReader.Structures.ExeFS;
using N3DSReader.Structures.NCCH;
using N3DSReader.Structures.NCSD;
using N3DSReader.Structures.RomFS;
using N3DSReader.Util;
using System.IO;

namespace N3DSReader
{
    class Program
    {
        static void Main(string[] args)
        {
            // string path = @"D:\Games\3DS\LEGO Star Wars III (USA).3ds";
            string path = @"D:\Games\3DS\pokiu_Edited.3ds";
            // string path = @"D:\Games\3DS\Mario Kart 7 (USA).3ds";

            FileStream fs = File.Open(path, FileMode.Open);
            NCSDHeader ncsdHeader = new NCSDHeader(IO.ReadData(fs, 512));
            System.Console.WriteLine(ncsdHeader);

            System.Console.Write("Partition: ");
            int partition = int.Parse(System.Console.ReadLine());
            int offset = ncsdHeader.PartitionTable.partitions[partition].Offset;

            fs.Seek(offset, SeekOrigin.Begin);
            NCCHHeader ncchHeader = new NCCHHeader(IO.ReadData(fs, 512));
            System.Console.WriteLine(ncchHeader);

            fs.Seek(ncchHeader.ExeFSOffset + offset, SeekOrigin.Begin);
            ExeFSHeader exeFSHeader = new ExeFSHeader(IO.ReadData(fs, 512));
            System.Console.WriteLine($"\nExeFS Header\n{exeFSHeader}");

            fs.Seek(ncchHeader.RomFSOffset + offset, SeekOrigin.Begin);
            IVFCHeader ivfcHeader = new IVFCHeader(IO.ReadData(fs, 92));
            System.Console.WriteLine($"\nIVFC Header (RomFS)\n{ivfcHeader}");

            int l3offset = ncchHeader.RomFSOffset + offset + 0x1000;
            L3Partition l3Partition = new L3Partition(fs, l3offset);
            System.Console.WriteLine($"\nL3 Header (RomFS)\n{l3Partition.Header}");
            System.Console.WriteLine($"\nL3 Partition Directory Metadata Table (RomFS)\n{l3Partition.DirectoryMetadataTable}");
            System.Console.WriteLine($"\nL3 Partition Files Metadata Table (RomFS)\n{l3Partition.FileMetadataTable}");

            /* foreach (PartitionMetadata partition in ncsdHeader.PartitionTable.partitions)
            {
                fs.Seek(partition.Offset, SeekOrigin.Begin);
                NCCHHeader ncchHeader = new NCCHHeader(ReadData(fs, 512));
                System.Console.WriteLine(ncchHeader);
            } */
        }
    }
}
