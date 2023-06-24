using Avesta.Security.Attribute;
using Avesta.Security.Interface;
using RestSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Security
{



    public class AVSSecurityEntityIOC : AVSSecurityEntity, IMatchable
    {
        public AVSSecurityEntityIOC(IModifier modifier) : base(modifier)
        {
        }
        public AVSSecurityEntityIOC()
        {
        }

        public override bool TryMatch(params ISecurityEntity[] keys)
        {
            ValidateForMaxCall(nameof(AVSSecurityEntity.TryMatch));
            return base.TryMatch(keys);
        }
        public override byte[] Matching(params ISecurityEntity[] keys)
        {
            ValidateForMaxCall(nameof(AVSSecurityEntity.Matching));
            return base.Matching(keys);
        }






        private void ValidateForMaxCall(string method)
        {
            base.IncreaseCall(method);
            var max = typeof(AVSSecurityEntity).GetMethod(method)?.GetAttribute<MaxCallAttribute>();
            var num = base.Values.Single(v => v.method.ToLower() == method.ToLower()).numberOfTry;
            if (num > max?.MAXTry)
                throw new Exception();
        }



    }



}
