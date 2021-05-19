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

        public static string FormatSize(long size)
        {
            if (size < 1024)
                return $"{size} Bytes";
            else if (size < 1048576)
                return $"{size / 1024} KB";
            else if (size < 1073741824)
                return $"{size / 1048576} MB";
            else
                return $"{size / 1073741824} GB";
        }
    }
}
