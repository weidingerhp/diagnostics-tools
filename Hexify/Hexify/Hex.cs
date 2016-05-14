using System;
using System.IO;
using System.Text;

namespace LowLevelDesign.Hexify
{
    public static class Hex
    {
        private readonly static HexEncoder encoder = new HexEncoder();

        /// <summary>
        /// Returns hex representation of the byte array.
        /// </summary>
        /// <param name="data">bytes to encode</param>
        /// <returns></returns>
        public static string ToHexString(byte[] data)
        {
            return ToHexString(data, 0, data.Length);
        }

        /// <summary>
        /// Returns hex representation of the byte array.
        /// </summary>
        /// <param name="data">bytes to encode</param>
        /// <param name="off">offset</param>
        /// <param name="length">number of bytes to encode</param>
        /// <returns></returns>
        public static string ToHexString(byte[] data, int off, int length)
        {
            return Encoding.ASCII.GetString(Encode(data, off, length));
        }

        private static byte[] Encode(byte[] data, int off, int length)
        {
            using (var stream = new MemoryStream()) {
                encoder.Encode(data, off, length, stream);
                return stream.ToArray();
            }
        }

        /// <summary>
        /// Decodes hex representation to a byte array.
        /// </summary>
        /// <param name="hex">hex string to decode</param>
        /// <returns></returns>
        public static byte[] FromHexString(string hex)
        {
            using (var stream = new MemoryStream()) {
                encoder.DecodeString(hex, stream);
                return stream.ToArray();
            }
        }

        /// <summary>
        /// Returns a string containing a nice representation  of the byte array 
        /// (similarly to the binary editors). 
        /// 
        /// Example output:
        /// 
        /// 
        /// </summary>
        /// <param name="bytes">array of bytes to pretty print</param>
        /// <returns></returns>
        public static string PrettyPrint(byte[] bytes)
        {
            if (bytes.Length == 0) {
                return string.Empty;
            }

            var buffer = new StringBuilder(); 
            int offset = 0;
            int end = Math.Min(16, bytes.Length);

            while (end <= bytes.Length) {
                // print offset 
                buffer.Append($"{offset:x4}:");

                // print hex bytes
                for (int i = offset; i < end; i++) {
                    buffer.Append($" {bytes[i]:x2}");
                }
                for (int i = 0; i < 16 - (end - offset); i++) {
                    buffer.Append("   ");
                }

                buffer.Append("  ");
                // print ascii characters
                for (int i = offset; i < end; i++) {
                    char c = (char)bytes[i];
                    if (char.IsLetterOrDigit(c) || char.IsPunctuation(c)) {
                        buffer.Append($"{c}");
                    } else {
                        buffer.Append(".");
                    }
                }

                if (end == bytes.Length) {
                    break;
                }

                offset = end;
                end = Math.Min(end + 16, bytes.Length);
                buffer.AppendLine();
            }

            return buffer.ToString();
        }
    }
}
