using Avesta.Security.AES.GCM;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Security.Test
{
    public class ThreeAESGCMUnitTest
    {

        [TestCaseSource(nameof(ParentOfThree))]
        public void Encrypted_Data_And_Decrypted_Data_Should_Be_Same(byte[] value, byte[] key, int numberOfChild)
        {
            var childs = TreeAESGCMService.GetChilds(value, key, numberOfChild);
            var parent = TreeAESGCMService.GetGodOfThree(childs);

            Assert.That(parent.Value, Is.EqualTo(value));
            Assert.That(parent.Key, Is.EqualTo(key));
        }



        static IEnumerable<TestCaseData> ParentOfThree()
        {
            return new List<TestCaseData>
            {
                new TestCaseData(UTF32Encoding.UTF8.GetBytes("KSJDU&$*@71738177363627364"),UTF32Encoding.UTF8.GetBytes("PASSWORD1"),2),
                new TestCaseData(UTF32Encoding.UTF8.GetBytes("KSJDU&$*@7173817asdfasdfa7363627364"),UTF32Encoding.UTF8.GetBytes("PASSWORasdfasdfaD1"),2),
                new TestCaseData(UTF32Encoding.UTF8.GetBytes("KSJDU&$*@7173817asdfasdfa7363627364"),UTF32Encoding.UTF8.GetBytes("PASSWORasdfasdfaD1"),5),
                new TestCaseData(UTF32Encoding.UTF8.GetBytes("dfsd-sdf-s-3-43-df--dfd"),UTF32Encoding.UTF8.GetBytes("&$^ydhh!"),5),
                new TestCaseData(UTF32Encoding.UTF8.GetBytes("dfsd-sdf-s-3-43-df--dfd"),UTF32Encoding.UTF8.GetBytes("&$^ydhh!"),3),
                new TestCaseData(UTF32Encoding.UTF8.GetBytes("TScTRHQsxZ13xPbCgVPiBjNiWZuK2NP2zs"),UTF32Encoding.UTF8.GetBytes("JKDOIEURH9849*"),10),
                new TestCaseData(UTF32Encoding.UTF8.GetBytes("sdfasdfasdfas0fxasdsfdsadfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdf10aa24612b26356318906300b8a9bc490a8f293f9ab6b7d8aa22de3044e2a6664732661rtfsgd6%%#^rtfsgd6%%#^664732661df!c8")
                    ,UTF32Encoding.UTF8.GetBytes("LDKOASIEJjhdhfjshhheyytwtqrag1332!@rtfsgd6%%#^664732661rtfsgd6%%#^rtfsgd6%%#^664732661df!664732661rtfsgd6%%#^rtfsgd6%%#^664732661df!664732661rtfsgd6%%#^rtfsgd6%%#^664732661df!664732661rtfsgd6%%#^rtfsgd6%%#^664732661df!"),50),
                new TestCaseData(UTF32Encoding.UTF8.GetBytes("sdfasdfasdfas0fxasdsfdsadfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdf10aa24612b26356318906300b8a9bc490a8f293f9ab6b7d8aa22de3044e2a6664732661rtfsgd6%%#^rtfsgd6%%#^664732661df!c8")
                    ,UTF32Encoding.UTF8.GetBytes("LDKOASIEJjhdhfjshhheyytwtqrag1332!@rtfsgd6%%#^664732661rtfsgd6%%#^rtfsgd6%%#^664732661df!664732661rtfsgd6%%#^rtfsgd6%%#^664732661df!664732661rtfsgd6%%#^rtfsgd6%%#^664732661df!664732661rtfsgd6%%#^rtfsgd6%%#^664732661df!"),34),
            };
        }


    }

}
