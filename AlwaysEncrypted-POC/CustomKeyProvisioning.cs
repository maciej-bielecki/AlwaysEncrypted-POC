using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace AlwaysEncrypted_POC
{
    public static class CustomKeyProvisioning
    {
        public static byte[] GetEncryptedColumnEncryptonKey()
        {
            int cekLength = 32;

            byte[] columnEncryptionKey = new byte[cekLength];
            RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();
            rngCsp.GetBytes(columnEncryptionKey);

            var provider = new CustomKeyStoreProvider();
            return provider.EncryptColumnEncryptionKey("path/to/nowhere", "shouldBe_RSA_OAEP", columnEncryptionKey);
        }

        public static string GetBitConvertedEncryptedColumnEncryptonKey()
        {
            byte[] EncryptedColumnEncryptionKey = GetEncryptedColumnEncryptonKey();
            var result = "0x" + BitConverter.ToString(EncryptedColumnEncryptionKey).Replace("-", "");
            return result;
        }
    }
}
