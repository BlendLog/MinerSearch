using System;
using System.IO;
using System.Security.Cryptography;

namespace DBase
{

    public static class Bfs
    {

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

        public static byte[] ConvertBase64ToBytes(string base64String)
        {
            return Convert.FromBase64String(base64String);
        }
    }
}
