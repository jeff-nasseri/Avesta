using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Helper
{


    public interface IA : IS
    {
        void A();
    }

    public interface IB : IS
    {
        void B();
    }

    public interface IC : IS
    {
        void C();
    }

    public interface IS 
    {
        void X();
    }
    public interface IFather
    {
        void Father();
    }
    public abstract class FatherClass : IFather
    {
        public void Father()
        {
        }
    }


    public class Service : FatherClass, IS
    {
        public void X()
        {
        }
    }



    public class AService : Service, IA
    {
        public void A()
        {
        }
    }

    public class BService : Service, IB
    {
        public void B()
        {
        }
    }


    public class CService : AService, IC
    {
        public void C()
        {
        }
    }



    public class Tagify
    {
        class Model
        {
            public string Value { get; set; }
        }
        public static IEnumerable<string> ConvertFromTagifyStr(string tagifyValue)
        {
            var models = JsonConvert.DeserializeObject<IEnumerable<Model>>(tagifyValue);
            var result = new List<string>();

            foreach (var item in models)
            {
                result.Add(item.Value);
            }
            return result;

        }

        public static string GetTagifyJSON(IEnumerable<string> list)
        {
            var models = list.Select(i => new Model { Value = i });
            var json = JsonConvert.SerializeObject(models);
            return json;
        }
    }
}
