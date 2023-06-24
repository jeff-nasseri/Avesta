using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Avesta.Share.Extensions;
using System.Threading.Tasks;
using Avesta.Security.Model;

namespace Avesta.Security.AES.GCM
{

    public class TreeAESGCMModel : SecurityBaseModel
    {
        public TreeAESGCMModel(int hLevel, int vLevel, int zLevel, byte[] value, byte[] key, int number)
        {
            HLevel = hLevel;
            Vlevel = vLevel;
            ZLevel = zLevel;
            Value = value;
            Key = key;
            Number = number;
        }

        public int HLevel { get; set; }
        public int Vlevel { get; set; }
        public int ZLevel { get; set; }
        public byte[] Value { get; set; }
        public byte[] Key { get; set; }
        public int Number { get; set; }
    }



    public class TreeAESGCMService
    {

        public static TreeAESGCMModel GetGodOfThree(IEnumerable<TreeAESGCMModel> childs)
        {
            if (childs.Count() < 2)
                return childs.First();

            var order = childs.OrderByDescending(child => child.ZLevel).GroupBy(c => c.ZLevel).ToList();
            var currentPair = order.First();

            var leftChild = currentPair.Single(node => node.HLevel == (0));
            var rightNode = currentPair.Single(node => node.HLevel == (1));

            var encryptedValue = leftChild.Value.Concat(rightNode.Value).ToArray();
            var encryptekey = leftChild.Key.Concat(rightNode.Key).ToArray();

            var aes_gcm = new AesGcmService();
            var key = aes_gcm.Decrypt(encryptekey, encryptedValue);
            var value = aes_gcm.Decrypt(encryptedValue, key);

            var zt = currentPair.Key % 2;
            var zLevel = (currentPair.Key - zt) / 2;
            var vLevel = leftChild.Vlevel - 1;

            var isThereAnyPartner = childs.Any(c => c.ZLevel == zLevel);
            var hLevel = isThereAnyPartner ? 0 : 1;

            var parent = new TreeAESGCMModel(hLevel, vLevel, zLevel, value, key, 2 * zLevel - (hLevel));

            var result = childs.Where(c => c.ID != leftChild.ID && c.ID != rightNode.ID).ToList();
            result.Add(parent);

            return GetGodOfThree(result);

        }

        public static IEnumerable<TreeAESGCMModel> GetChilds(byte[] value, byte[] key, int numberOfChild = 2)
        {

            if (numberOfChild < 2)
                throw new Exception();

            var numberOfSplit = numberOfChild - 1;
            var target = new List<TreeAESGCMModel>();
            var vCounter = 0;

            var hLevel = 0;
            var vLevel = 0;
            var zLevel = 0;
            var nodeNumber = 0;

            var firstNode = new TreeAESGCMModel(hLevel, vLevel, zLevel, value, key, nodeNumber);
            target.Add(firstNode);


            for (int i = 0; i < numberOfSplit; i++)
            {

                zLevel++;

                if (vCounter >= target.Count)
                {
                    vCounter = 0;
                    vLevel++;
                }

                var order = target.OrderBy(node => node.ZLevel).GroupBy(nodes => nodes.ZLevel);
                var pair = order.First();
                var currentNode = pair.OrderBy(child => child.HLevel).First();

                var aes_gcm = new AesGcmService();
                var encryptedValueOfCurrentNode = aes_gcm.Encrypt(currentNode.Value, currentNode.Key);
                var encryptedKeyOfCurrentNode = aes_gcm.Encrypt(currentNode.Key, encryptedValueOfCurrentNode);

                var splitedValueForChild = encryptedValueOfCurrentNode.Split();
                var splitedKeyForChild = encryptedKeyOfCurrentNode.Split();


                target.Remove(currentNode);

                var leftNode = new TreeAESGCMModel(hLevel: (0), vLevel, zLevel, splitedValueForChild.First(), splitedKeyForChild.First(), 2 * zLevel + (0));
                var rightNode = new TreeAESGCMModel(hLevel: (1), vLevel, zLevel, splitedValueForChild.Last(), splitedKeyForChild.Last(), 2 * zLevel + (1));
                target.Add(leftNode);
                target.Add(rightNode);

                vCounter += 2;
            }

            return target;

        }




        public static void Show(IEnumerable<TreeAESGCMModel> models)
        {
            throw new NotImplementedException();
        }






    }


}
