using System;
using System.Collections.Generic;
using System.Text;

namespace N3DSReader.Structures.NCSD
{
    public class CryptTable
    {
        List<int> partitions = new List<int>(8);

        public CryptTable(byte[] bytes)
        {
            if (bytes.Length != 8) throw new ArgumentException("There should be 8 bytes!");
            foreach (byte b in bytes)
            {
                partitions.Add(b);
            }
        }
    }
}
