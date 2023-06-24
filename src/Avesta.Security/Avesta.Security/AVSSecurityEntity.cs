using Avesta.Security.Attribute;
using Avesta.Security.Interface;
using Avesta.Security.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Security
{

    public abstract class AVSSecurityEntity : SecurityBaseModel, ISecurityEntity, IMaxCallProvider
    {
        public AVSSecurityEntity(IModifier modifier)
        {
            this.Modifier = modifier;
        }
        public AVSSecurityEntity()
        {
        }
        public string Name { get; set; }
        public IModifier Modifier { get; private set; }



        [MaxCall(MAXTry = 10)]
        public virtual byte[] Matching(params ISecurityEntity[] keys)
        {
            var modifiers = keys.Select(m => m.Modifier).ToArray();
            var result = this.Modifier.Match(modifiers);
            return result;
        }

        [MaxCall(MAXTry = 10)]
        public virtual bool TryMatch(params ISecurityEntity[] keys)
        {
            var modifiers = keys.Select(m => m.Modifier).ToArray();
            var result = this.Modifier.IsMatch(modifiers);
            return result;
        }


        public ISecurityEntity SetModifier(IModifier modifier)
        {
            this.Modifier = modifier;
            return this;
        }



        public IEnumerable<(string method, int numberOfTry)> Values { get; private set; }
        public void IncreaseCall(string method)
        {
            Values ??= new List<(string, int)>();
            var target = Values.SingleOrDefault(v => v.method.ToLower() == method.ToLower());
            if (target == default((string, int)))
                Values.ToList().Add((method, 0));
            else
            {
                var num = target.numberOfTry;
                num++;
                Values.ToList().Remove(target);
                Values.ToList().Add((method, num));
            }

        }




        public bool Equals(ISecurityEntity? other) => this.Modifier.Equals(other?.Modifier);


    }

}
