using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace N3DSReader.Util
{
    class Hex
    {
        public static string Format(byte[] bytes) =>
            bytes.Aggregate("", (acc, x) => acc + string.Format("{0:X2}", x));
        public static string Format(byte[] bytes, int start, int size) => Format(bytes[start..(start + size)]);

        public static int ParseInt16(byte[] bytes) => BitConverter.ToInt16(bytes);
        public static int ParseInt16(byte[] bytes, int start, int size) => ParseInt16(bytes[start..(start + size)]);

        public static int ParseInt(byte[] bytes) =>
            bytes.Length > 1 ? BitConverter.ToInt32(bytes) : Convert.ToInt32(bytes[0]);
        public static int ParseInt(byte[] bytes, int start, int size) => ParseInt(bytes[start..(start + size)]);

        public static string ToString(byte[] bytes) => Encoding.UTF8.GetString(bytes, 0, bytes.Length).TrimEnd('\0');
        public static string ToString(byte[] bytes, int start, int size) => ToString(bytes[start..(start + size)]);

        public static string ToUnicode(byte[] bytes) => Encoding.UTF8.GetString(bytes, 0, bytes.Length).TrimEnd('\0');
        public static string ToUnicode(byte[] bytes, int start, int size) => ToUnicode(bytes[start..(start + size)]);
    }
}
