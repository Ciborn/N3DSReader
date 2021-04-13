using System.IO;

namespace N3DSReader.Util
{
    class IO
    {
        public static byte[] ReadData(FileStream fs, int size)
        {
            byte[] data = new byte[size];
            fs.Read(data, 0, size);
            return data;
        }
    }
}
