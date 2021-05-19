using N3DSReader.Util;
using System;
using System.Collections.Generic;

namespace N3DSReader.Structures.ExeFS
{
    class ExeFSHeader
    {
        public List<ExeFSFileHeader> FileHeaders { get; } = new List<ExeFSFileHeader>(); // 0x000, 0x0a0
        public List<string> FileHashes { get; } = new List<string>();                    // 0x0c0, 0x140
        public long Offset { get; set; }

        public ExeFSHeader(byte[] bytes, long offset)
        {
            if (bytes.Length != 512) throw new ArgumentException("There should be 512 bytes!");
            Offset = offset;
            for (int i = 0; i < 10; i++)
            {
                byte[] fileHeader = bytes[(i * 16)..(i * 16 + 16)];
                byte[] fileHash = bytes[(0xc0 + i * 16)..(0xd0 + i * 16)];
                if (fileHeader[0] != 0)
                {
                    FileHeaders.Add(new ExeFSFileHeader(fileHeader, offset));
                    FileHashes.Add(Hex.Format(fileHash));
                }
            }
        }

        public override string ToString()
        {
            return string.Join("\n", FileHeaders);
        }
    }
}
