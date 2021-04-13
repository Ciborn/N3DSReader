using System;
using System.Collections.Generic;
using System.Linq;

namespace N3DSReader.Structures.NCSD
{
    public class PartitionTable
    {
        public List<PartitionMetadata> partitions = new List<PartitionMetadata>(8);

        public PartitionMetadata CXI { get => partitions[0]; }
        public PartitionMetadata EManual { get => partitions[1]; }
        public PartitionMetadata DownloadPlay { get => partitions[2]; }
        public PartitionMetadata N3DSUpdateData { get => partitions[6]; }
        public PartitionMetadata UpdateData { get => partitions[7]; }

        public PartitionTable(byte[] bytes)
        {
            if (bytes.Length != 64) throw new ArgumentException("There should be 64 bytes!");
            for (int i = 0; i < 8; i++){
                partitions.Add(new PartitionMetadata(i, bytes[(i * 8)..(i * 8 + 8)].ToArray()));
            }
        }

        public override string ToString()
        {
            return string.Join("\n                 ", partitions);
        }
    }
}
