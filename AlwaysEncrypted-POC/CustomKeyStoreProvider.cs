using Microsoft.Data.SqlClient;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace AlwaysEncrypted_POC
{
    public class CustomKeyStoreProvider : SqlColumnEncryptionKeyStoreProvider
    {
        public static readonly string ProviderName = "CUSTOM_KEY_STORE";

        public override byte[] DecryptColumnEncryptionKey(string masterKeyPath, string encryptionAlgorithm, byte[] encryptedColumnEncryptionKey)
        {
            using RijndaelManaged rijAlg = new RijndaelManaged();
            using (var keyBytes = new Rfc2898DeriveBytes(GetSuperSecretMasterKeyByPath(), GetSuperSecretSalt(), 1000))
            {
                rijAlg.Mode = CipherMode.CBC;
                rijAlg.KeySize = 256;
                rijAlg.BlockSize = 128;
                rijAlg.Key = keyBytes.GetBytes(rijAlg.KeySize / 8);
                rijAlg.IV = keyBytes.GetBytes(rijAlg.BlockSize / 8);
            }

            using var msDecrypt = new MemoryStream();
            using (var csDecrypt = new CryptoStream(msDecrypt, rijAlg.CreateDecryptor(), CryptoStreamMode.Write))
            {
                csDecrypt.Write(encryptedColumnEncryptionKey, 0, encryptedColumnEncryptionKey.Length);
                csDecrypt.Close();
            }
            var decryptedColumnEncryptionKey = msDecrypt.ToArray();
            return decryptedColumnEncryptionKey;
        }


        public override byte[] EncryptColumnEncryptionKey(string masterKeyPath, string encryptionAlgorithm, byte[] columnEncryptionKey)
        {
            using RijndaelManaged rijAlg = new RijndaelManaged();
            using (var keyBytes = new Rfc2898DeriveBytes(GetSuperSecretMasterKeyByPath(), GetSuperSecretSalt(), 1000))
            {
                rijAlg.Mode = CipherMode.CBC;
                rijAlg.KeySize = 256;
                rijAlg.BlockSize = 128;
                rijAlg.Key = keyBytes.GetBytes(rijAlg.KeySize / 8);
                rijAlg.IV = keyBytes.GetBytes(rijAlg.BlockSize / 8);
            }

            using var msEncrypt = new MemoryStream();
            using (var csEncrypt = new CryptoStream(msEncrypt, rijAlg.CreateEncryptor(), CryptoStreamMode.Write))
            {
                csEncrypt.Write(columnEncryptionKey, 0, columnEncryptionKey.Length);
                csEncrypt.Close();
            }
            var encryptedBytes = msEncrypt.ToArray();
            return encryptedBytes;
        }

        private byte[] GetSuperSecretSalt()
        {
            return UTF8Encoding.UTF8.GetBytes("superSalt");
        }

        private string GetSuperSecretMasterKeyByPath()
        {
            return "superPassword";
        }
    }
}