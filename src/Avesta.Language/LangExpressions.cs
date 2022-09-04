using Avesta.Global;
using System;
using System.Collections.Generic;

namespace Avesta.Language
{
    public class LangExpressions : EnhancedDictionary<string, LangExpression>
    {
        public new LangExpression this[string key]
        {
            get
            {
                return base[key];
            }
            set
            {
                if (this[key] != null)
                    throw new Exception($"Lang expression '{key}' is already defined.");
                base[key] = value;
            }
        }
    }
}

