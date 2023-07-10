using Avesta.Security.Hash;
using Avesta.Share.Utilities;
using NUnit.Framework;
using System.Collections.ObjectModel;

namespace Avesta.Security.Test
{
    public class Sha256UnitTest
    {
        [TestCase("Alireza1", "3051571b380d0c3b56163d1bbeee6f24fe3cd39145083f94a9e6e3db2ed636f4")]
        [TestCase("Pizza Dost Daram", "2d48c3d9e5ecc35329ac8db36bc5fca0e9908c68e34c8cdea1008d6e89daddb0")]
        [TestCase("SamplePassword123!@#", "bdd8caeedb91f7962dc1416f4dece6cac7e6f91b01905fcac891cb361377a9d0")]
        [TestCase("DFJS*#&#(@))!*(#$*&Djsdkfladfjdnmcbwjakfgda!347809(0-", "b950723a2f5c2a1600989fd9f3c20395709f0dfbf7297fa3c5c6f9c2353dba1b")]
        public void Sha256Test(string plain, string finger)
        {
            ReadOnlyCollection<byte> hash = Sha256.HashStream(StringUtls.GenerateStreamFromString(plain));

            var output = ArrayUtls.ArrayToString(hash);
            Assert.That(finger, Is.EqualTo(output));
        }

    }

}
