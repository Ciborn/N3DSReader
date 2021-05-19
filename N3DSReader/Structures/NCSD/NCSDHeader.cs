using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using N3DSReader.Util;

namespace N3DSReader.Structures.NCSD
{
    public enum FSType
    {
        None,
        Normal,
        FIRM,
        AGB_FIRM
    }

    public class NCSDHeader
    {
        public string Signature { get; }              // 0x000, 0x100
        public string MagicNumber { get; }            // 0x100, 0x004
        public long Size { get; }                     // 0x104, 0x004
        public int MediaID { get; }                   // 0x108, 0x008
        public FSType FSType { get; }                 // 0x110, 0x008
        public CryptTable CryptTable { get; }         // 0x118, 0x008
        public PartitionTable PartitionTable { get; } // 0x120, 0x040
        public long Offset { get; set; }

        public NCSDHeader(byte[] bytes, long offset = 0)
        {
            if (bytes.Length != 512) throw new ArgumentException("There should be 512 bytes!");
            Offset = offset;
            Signature = Hex.Format(bytes, 0x000, 0x100);
            MagicNumber = Hex.ToString(bytes, 0x100, 0x004);
            Size = Hex.ParseInt(bytes, 0x104, 0x004) * (long)512;
            MediaID = Hex.ParseInt(bytes, 0x108, 0x008);
            FSType = (FSType)Hex.ParseInt(bytes, 0x110, 0x008);
            CryptTable = new CryptTable(bytes[280..288]);
            PartitionTable = new PartitionTable(bytes[288..352]);
        }

        public override string ToString()
        {
            string signature = Regex.Replace(Signature, "(.{" + 32 + "})", "$1" + "\n                 ");
            return string.Join(Environment.NewLine,
                $"Signature      : {signature[0..^18]}",
                $"Magic Number   : {MagicNumber}",
                $"Size           : {Size / 0x100000} MB",
                $"MediaID        : {MediaID}",
                $"FS Type        : {FSType}",
                $"Crypt Table    : {CryptTable}",
                $"Partition Table: {PartitionTable}"
            );
        }
    }
}
