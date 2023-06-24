using Avesta.Security.AES.GCM;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Security.Test
{
    public class AESGCMUnitTest
    {
     

        [TestCaseSource(nameof(EncryptCase))]
        public void Encrypt_And_Decrypt_Result_Should_Be_The_First_Value(byte[] first, byte[] password)
        {
            var _service = new AesGcmService();

            var enc = _service.Encrypt(first, password);
            var dec = _service.Decrypt(enc, password);

            Assert.AreEqual(dec, first);

            _service.Dispose();
        }

        class ItemModel
        {
            public string Input { get; set; }
            public string Password { get; set; }
        }
        static IEnumerable<TestCaseData> EncryptCase()
        {
            var data = new List<ItemModel>()
            {
                new ItemModel
                {
                    Input = "dfajsdfkljash ",
                    Password = "Alidfs239!",
                },
                new ItemModel
                {
                    Input = "##@$kj34kj@KJ#h4234234",
                    Password = " _ _ =d-s - )",
                },
                new ItemModel
                {
                    Input = "fsdf43r09p43590",
                    Password = "SFLKJls9",
                }
            };

            var result = data.Select(item => new TestCaseData(UTF32Encoding.UTF8.GetBytes(item.Input),UTF32Encoding.UTF8.GetBytes(item.Password))).ToList();
            return result;
        }



    }

}
