using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace MinerSearch
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
            internal int memid(int int_0, char char_0)
            {
                return int_0 + (int)char_0;
            }

            internal get_DynamicDirectory()
            {
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

        public static string Create(string base64CipherText, byte[] Key, byte[] IV)
        {
            byte[] cipherText = Convert.FromBase64String(base64CipherText);
            string plaintext = null;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

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

        public static byte[] DecryptBytes(byte[] cipherBytes, byte[] key, byte[] iv)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(cipherBytes))
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
    }
}
