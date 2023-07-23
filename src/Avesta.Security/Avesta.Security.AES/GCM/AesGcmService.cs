using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Security.AES.GCM
{

    public class AesGcmService : IDisposable
    {
        private AesGcm _aes;

        public void SetPassword(byte[] password)
        {
            byte[] key = new Rfc2898DeriveBytes(password + "**********", new byte[8], 1000, HashAlgorithmName.SHA256).GetBytes(16);
            _aes = new AesGcm(key);
        }


        public byte[] Encrypt(byte[] plainBytes, byte[] password)
        {

            SetPassword(password);

            int nonceSize = AesGcm.NonceByteSizes.MaxSize;
            int tagSize = AesGcm.TagByteSizes.MaxSize;

            int cipherSize = plainBytes.Length;
            int encryptedDataLength = 4 + nonceSize + 4 + tagSize + cipherSize;
            Span<byte> encryptedData = encryptedDataLength < 1024 ? stackalloc byte[encryptedDataLength] : new byte[encryptedDataLength].AsSpan();

            BinaryPrimitives.WriteInt32LittleEndian(encryptedData.Slice(0, 4), nonceSize);
            BinaryPrimitives.WriteInt32LittleEndian(encryptedData.Slice(4 + nonceSize, 4), tagSize);
            var nonce = encryptedData.Slice(4, nonceSize);
            var tag = encryptedData.Slice(4 + nonceSize + 4, tagSize);
            var cipherBytes = encryptedData.Slice(4 + nonceSize + 4 + tagSize, cipherSize);

            RandomNumberGenerator.Fill(nonce);

            _aes.Encrypt(nonce, plainBytes.AsSpan(), cipherBytes, tag);

            return encryptedData.ToArray();

        }
       

        public byte[] Decrypt(byte[] data, byte[] password)
        {
            SetPassword(password);

            Span<byte> encryptedData = data.AsSpan();

            int nonceSize = BinaryPrimitives.ReadInt32LittleEndian(encryptedData.Slice(0, 4));
            int tagSize = BinaryPrimitives.ReadInt32LittleEndian(encryptedData.Slice(4 + nonceSize, 4));
            int cipherSize = encryptedData.Length - 4 - nonceSize - 4 - tagSize;

            var nonce = encryptedData.Slice(4, nonceSize);
            var tag = encryptedData.Slice(4 + nonceSize + 4, tagSize);
            var cipherBytes = encryptedData.Slice(4 + nonceSize + 4 + tagSize, cipherSize);

            Span<byte> plainBytes = cipherSize < 1024 ? stackalloc byte[cipherSize] : new byte[cipherSize];
            _aes.Decrypt(nonce, cipherBytes, tag, plainBytes);

            return plainBytes.ToArray();
        }

      
        public void Dispose()
        {
            _aes.Dispose();
        }
    }



}
