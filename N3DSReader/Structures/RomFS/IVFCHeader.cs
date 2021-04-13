using N3DSReader.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace N3DSReader.Structures.RomFS
{
    class IVFCHeader
    {
        public int MasterHashSize { get; }   // 0x08, 0x4
        public int L1Offset { get; }         // 0x0c, 0x8
        public int L1HashdataSize { get; }   // 0x14, 0x8
        public int L1BlockSize { get; }      // 0x1c, 0x4
        public int L2Offset { get; }         // 0x24, 0x8
        public int L2HashdataSize { get; }   // 0x2c, 0x8
        public int L2BlockSize { get; }      // 0x34, 0x4
        public int L3Offset { get; }         // 0x3c, 0x8
        public int L3HashdataSize { get; }   // 0x44, 0x8
        public int L3BlockSize { get; }      // 0x4c, 0x4
        public int OptionalInfoSize { get; } // 0x58, 0x4

        public IVFCHeader(byte[] bytes)
        {
            if (bytes.Length != 92) throw new ArgumentException("There should be 92 bytes!");
            MasterHashSize = Hex.ParseInt(bytes, 0x08, 0x4);
            L3Offset = Hex.ParseInt(bytes, 0x0c, 0x8);
            L1HashdataSize = Hex.ParseInt(bytes, 0x14, 0x8);
            L1BlockSize = Hex.ParseInt(bytes, 0x1c, 0x4);
            L2Offset = Hex.ParseInt(bytes, 0x24, 0x8);
            L2HashdataSize = Hex.ParseInt(bytes, 0x2c, 0x8);
            L2BlockSize = Hex.ParseInt(bytes, 0x34, 0x4);
            L1Offset = Hex.ParseInt(bytes, 0x3c, 0x8);
            L3HashdataSize = Hex.ParseInt(bytes, 0x44, 0x8);
            L3BlockSize = Hex.ParseInt(bytes, 0x4c, 0x4);
            OptionalInfoSize = Hex.ParseInt(bytes, 0x58, 0x4);
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine,
                $"Master Hash Size  : {MasterHashSize} Bytes",
                $"L1 Offset         : 0x{L1Offset:x8}",
                $"L1 Hashdata Size  : {L1HashdataSize / 1024} KB",
                $"L1 Block Size     : {L1BlockSize}",
                $"L2 Offset         : 0x{L2Offset:x8}",
                $"L2 Hashdata Size  : {L2HashdataSize / 0x100000} MB",
                $"L2 Block Size     : {L2BlockSize}",
                $"L3 Offset         : 0x{L3Offset:x8}",
                $"L3 Hashdata Size  : {L3HashdataSize / 0x100000} MB",
                $"L3 Block Size     : {L3BlockSize}",
                $"Optional Info Size: {OptionalInfoSize} Bytes"
            );
        }
    }
}
