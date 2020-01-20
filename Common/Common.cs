using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Common
{
    public class Common
    {
        // 将用户输入的密码的转换成MD5字节码 
        public static string getMd5Hash(string input)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.
            MD5 md5Hasher = MD5.Create();
            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();
            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            // Return the hexadecimal string.
            return sBuilder.ToString();
        }


        // Verify a hash against a string.
        public static bool verifyMd5Hash(string input, string hash)
        {
            // Hash the input.
            string hashOfInput = getMd5Hash(input);
            // Create a StringComparer an comare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void writeFile(string path, string content)
        {
            if (File.Exists(path))
            {
                // 写入文件内容                
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine(content);
                }
            }
        }

        /// <summary>
        /// 删除目录
        /// </summary>
        /// <returns></returns>
        public static bool delDirectory(string vPath)
        {
            try
            {
                if (System.IO.Directory.Exists(vPath))
                {
                    System.IO.Directory.Delete(vPath, true);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="vFile"></param>
        public static bool delFile(string vFile)
        {
            try
            {
                if (System.IO.File.Exists(vFile))
                {
                    System.IO.File.Delete(vFile);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
