using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace DBase
{

    public static class Bfs
    {
        public static string obfs(string string_0, object[] object_0)
        {
            MinNumberPairwiseScalar minNumberPairwiseScalar = new MinNumberPairwiseScalar();
            minNumberPairwiseScalar.DictionaryNode = 0;
            foreach (object obj in object_0)
            {
                string text = obj as string;
                if (text == null)
                {
                    if (obj is int)
                    {
                        int num = (int)obj;
                        minNumberPairwiseScalar.DictionaryNode += num;
                    }
                    else if (obj is char)
                    {
                        char c = (char)obj;
                        minNumberPairwiseScalar.DictionaryNode += (int)c;
                    }
                }
                else
                {
                    minNumberPairwiseScalar.DictionaryNode = text.Aggregate(minNumberPairwiseScalar.DictionaryNode, new Func<int, char, int>(get_DynamicDirectory.DangerousRelease.memid));
                }
            }
            return string_0.Aggregate(string.Empty, new Func<string, char, string>(minNumberPairwiseScalar.ComMemberType));
        }

        [Serializable]
        private sealed class get_DynamicDirectory
        {
            internal get_DynamicDirectory()
            {
            }
            internal int memid(int int_0, char char_0)
            {
                return int_0 + (int)char_0;
            }


            internal static readonly get_DynamicDirectory DangerousRelease = new get_DynamicDirectory();
        }

        private sealed class MinNumberPairwiseScalar
        {
            internal MinNumberPairwiseScalar()
            {
            }

            internal string ComMemberType(string string_0, char char_0)
            {
                return string_0 + ((char)((int)char_0 ^ this.DictionaryNode)).ToString();
            }

            internal int DictionaryNode;
        }

        public static string GetStr(string original, int numberToObf)
        {
            return obfs(original, new object[] { numberToObf });
        }

        public static string Create(string base64CipherText, string Key, string IV)
        {
            byte[] cipherText = Convert.FromBase64String(base64CipherText);
            string plaintext = null;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = ConvertBase64ToBytes(Key);
                aesAlg.IV = ConvertBase64ToBytes(IV);

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            return plaintext;
        }

        public static byte[] Decrypt(string cipherBase64, string key, string iv)
        {
            byte[] cipherText = Convert.FromBase64String(cipherBase64);
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = ConvertBase64ToBytes(key);
                aesAlg.IV = ConvertBase64ToBytes(iv);

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (MemoryStream msPlain = new MemoryStream())
                        {
                            csDecrypt.CopyTo(msPlain);
                            return msPlain.ToArray();
                        }
                    }
                }
            }
        }

        public static byte[] ConvertBase64ToBytes(string base64String)
        {
            return Convert.FromBase64String(base64String);
        }
    }
}
