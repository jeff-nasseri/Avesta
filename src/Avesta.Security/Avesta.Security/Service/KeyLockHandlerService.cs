using Avesta.Security.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace Avesta.Security.Service
{



    public interface IKeyLockHandler
    {
        IEnumerable<ISecurityEntity> MakeKeyLockPair();
        IEnumerable<ISecurityEntity> MakeKeyLockPairse(int number = 2);
    }


    public class KeyLockHandlerService : IKeyLockHandler
    {

        public virtual IEnumerable<ISecurityEntity> MakeKeyLockPair() => MakeKeyLockPairse();

        public virtual IEnumerable<ISecurityEntity> MakeKeyLockPairse(int number = 2)
        {
            var body = UTF32Encoding.UTF8.GetBytes(Guid.NewGuid().ToString());

            var godModifier = new Modifier("god", new ModifierKey(), body);
            var modifiers = godModifier.Split(number);

            string NAME = Guid.NewGuid().ToString();

            foreach (var modifier in modifiers)
            {
                yield return new AVSSecurityEntityIOC()
                {
                    Name = NAME,
                }.SetModifier(modifier);
            }
        }
    }
}
