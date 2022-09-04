using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Global
{
    public class EnhancedDictionary<TKey, TValue> : Dictionary<TKey, TValue>
    {
        #region Constructors
        public EnhancedDictionary() : base() { }
        public EnhancedDictionary(IDictionary<TKey, TValue> dictionary) : base(dictionary) { }
        public EnhancedDictionary(IEqualityComparer<TKey> comparer) : base(comparer) { }
        public EnhancedDictionary(int capacity) : base(capacity) { }
        public EnhancedDictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer) : base(dictionary, comparer) { }
        public EnhancedDictionary(int capacity, IEqualityComparer<TKey> comparer) : base(capacity, comparer) { }
        #endregion

        #region Properties
        public new TValue this[TKey key]
        {
            get
            {
                if (key != null && ContainsKey(key))
                    return base[key];
                else
                    return default(TValue);
            }
            set
            {
                if (key == null)
                    return;
                if (ContainsKey(key))
                    base[key] = value;
                else
                    Add(key, value);
            }
        }
        #endregion

        #region Methods
        public void RemoveIfExists(TKey key)
        {
            Remove(key);
        }
        #endregion
    }

}
