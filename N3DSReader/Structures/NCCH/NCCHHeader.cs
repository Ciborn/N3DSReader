using N3DSReader.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace N3DSReader.Structures.NCCH
{
    enum ContentPlatform
    {
        CTR = 1,
        snake = 2
    }

    [Flags]
    enum ContentType
    {
        Data = 1,
        Executable = 2,
        SystemUpdate = 4,
        Manual = 8,
        Trial = 10
    }

    [Flags]
    enum Bitmasks
    {
        FixedCryptoKey = 1,
        NoMountRomFs = 2,
        NoCrypto = 4
    }

    class NCCHHeader
    {
        public string Signature { get; }                // 0x000, 0x100
        public string MagicNumber { get; }              // 0x100, 0x004
        public long Size { get; }                       // 0x104, 0x004
        public int PartitionID { get; }                 // 0x108, 0x008
        public int MarkerCode { get; }                  // 0x110, 0x002
        public int Version { get; }                     // 0x112, 0x002
        public int ProgramID { get; }                   // 0x118, 0x008
        public string LogoRegionHash { get; }           // 0x130, 0x020
        public string ProductCode { get; }              // 0x150, 0x010
        public string ExtendedHeaderHash { get; }       // 0x160, 0x020
        public string ExtendedHeaderSize { get; }       // 0x180, 0x004
        public int CryptoMethod { get; }                // 0x18b, 0x001
        public ContentPlatform ContentPlatform { get; } // 0x18c, 0x001
        public ContentType ContentType { get; }         // 0x18d, 0x001
        public int ContentUnitSize { get; }             // 0x18e, 0x001
        public Bitmasks Bitmasks { get; }               // 0x18f, 0x001
        public long PlainRegionOffset { get; }           // 0x190, 0x004
        public long PlainRegionSize { get; }             // 0x194, 0x004
        public long LogoRegionOffset { get; }            // 0x198, 0x004
        public long LogoRegionSize { get; }              // 0x19c, 0x004
        public long ExeFSOffset { get; }                 // 0x1a0, 0x004
        public long ExeFSSize { get; }                   // 0x1a4, 0x004
        public long ExeFSHashRegionSize { get; }         // 0x1a8, 0x004
        public long RomFSOffset { get; }                 // 0x1b0, 0x004
        public long RomFSSize { get; }                   // 0x1b4, 0x004
        public long RomFSHashRegionSize { get; }         // 0x1b8, 0x004
        public string ExeFSSuperblockHash { get; }      // 0x1c0, 0x020
        public string RomFSSuperblockHash { get; }      // 0x1e0, 0x020
        public long Offset { get; set; }

        public NCCHHeader(byte[] bytes, long offset)
        {
            if (bytes.Length != 512) throw new ArgumentException("There should be 512 bytes!");
            Offset = offset;
            Signature = Hex.Format(bytes, 0x000, 0x100);
            MagicNumber = Hex.ToString(bytes, 0x100, 0x004);
            Size = Hex.ParseInt(bytes, 0x104, 0x004) * (long)512;
            PartitionID = Hex.ParseInt(bytes, 0x108, 0x008);
            MarkerCode = Hex.ParseInt16(bytes, 0x110, 0x002);
            Version = Hex.ParseInt16(bytes, 0x112, 0x002);
            ProgramID = Hex.ParseInt(bytes, 0x118, 0x008);
            LogoRegionHash = Hex.Format(bytes, 0x130, 0x020);
            ProductCode = Hex.ToString(bytes, 0x150, 0x010);
            ExtendedHeaderHash = Hex.Format(bytes, 0x160, 0x020);
            ExtendedHeaderSize = Hex.Format(bytes, 0x180, 0x004);
            CryptoMethod = Hex.ParseInt(bytes, 0x18b, 0x001);
            ContentPlatform = (ContentPlatform)Hex.ParseInt(bytes, 0x18c, 0x001);
            ContentType = (ContentType)Hex.ParseInt(bytes, 0x18d, 0x001);
            ContentUnitSize = Hex.ParseInt(bytes, 0x18e, 0x001);
            Bitmasks = (Bitmasks)Hex.ParseInt(bytes, 0x18f, 0x001);
            PlainRegionOffset = Hex.ParseInt(bytes, 0x190, 0x004) * 512 + Offset;
            PlainRegionSize = Hex.ParseInt(bytes, 0x194, 0x004) * (long)512;
            LogoRegionOffset = Hex.ParseInt(bytes, 0x198, 0x004) * 512 + Offset;
            LogoRegionSize = Hex.ParseInt(bytes, 0x19c, 0x004) * (long)512;
            ExeFSOffset = Hex.ParseInt(bytes, 0x1a0, 0x004) * 512 + Offset;
            ExeFSSize = Hex.ParseInt(bytes, 0x1a4, 0x004) * (long)512;
            ExeFSHashRegionSize = Hex.ParseInt(bytes, 0x1a8, 0x004) * (long)512;
            RomFSOffset = Hex.ParseInt(bytes, 0x1b0, 0x004) * 512 + Offset;
            RomFSSize = Hex.ParseInt(bytes, 0x1b4, 0x004) * (long)512;
            RomFSHashRegionSize = Hex.ParseInt(bytes, 0x1b8, 0x004) * (long)512;
            ExeFSSuperblockHash = Hex.Format(bytes, 0x1c0, 0x020);
            RomFSSuperblockHash = Hex.Format(bytes, 0x1e0, 0x020);
        }

        public override string ToString()
        {
            string signature = Regex.Replace(Signature, "(.{" + 32 + "})", "$1" + "\n                        ");
            return string.Join(Environment.NewLine,
                $"Signature             : {signature.Substring(0, signature.Length - 18)}",
                $"Magic Number          : {MagicNumber}",
                $"Size                  : {Size / 0x100000} MB",
                $"Partition ID          : {PartitionID}",
                $"Marker Code           : {MarkerCode}",
                $"Version               : {Version}",
                $"Logo Region Hash      : {LogoRegionHash}",
                $"Product Code          : {ProductCode}",
                $"Extended Header Hash  : {ExtendedHeaderHash}",
                $"Extended Header Size  : {ExtendedHeaderSize}",
                $"Crypto Method         : {CryptoMethod}",
                $"Content Platform      : {ContentPlatform}",
                $"Content Type          : {ContentType}",
                $"Content Unit Size     : {ContentUnitSize}",
                $"Bitmasks              : {Bitmasks}",
                $"Plain Region Offset   : 0x{PlainRegionOffset:x8}",
                $"Plain Region Size     : {PlainRegionSize} Bytes",
                $"Logo Region Offset    : 0x{LogoRegionOffset:x8}",
                $"Logo Region Size      : {LogoRegionSize} Bytes",
                $"ExeFS Offset          : 0x{ExeFSOffset:x8}",
                $"ExeFS Size            : {ExeFSSize / 0x100000} MB",
                $"ExeFS Hash Region Size: {ExeFSHashRegionSize} Bytes",
                $"RomFS Offset          : 0x{RomFSOffset:x8}",
                $"RomFS Size            : {RomFSSize / 0x100000} MB",
                $"RomFS Hash Region Size: {RomFSHashRegionSize} Bytes",
                $"ExeFS Superblock Hash : {ExeFSSuperblockHash}",
                $"RomFS Superblock Hash : {RomFSSuperblockHash}"
            );
        }
    }
}
