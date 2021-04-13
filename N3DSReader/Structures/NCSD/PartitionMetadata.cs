using N3DSReader.Constants;
using System;
using System.Linq;

namespace N3DSReader.Structures.NCSD
{
    public class PartitionMetadata
    {
        public int Id { get; }
        public string Name { get; }
        public int Offset { get; }
        public int Length { get; }

        public PartitionMetadata(int id, byte[] bytes)
        {
            if (bytes.Length != 8) throw new ArgumentException("There should be 8 bytes!");
            Id = id;
            Name = Partitions.Names[Id];
            Offset = BitConverter.ToInt32(bytes[0..4].ToArray()) * 512;
            Length = BitConverter.ToInt32(bytes[4..8].ToArray()) * 512;
        }

        public override string ToString()
        {
            return $"Part{Id} {$"({Name})",-16} // Offset: 0x{Offset:x8}, Size: {Length / 0x100000} MB";
        }
    }
}
