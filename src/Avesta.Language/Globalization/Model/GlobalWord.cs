using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Language.Globalization.Model
{
    public class GlobalWord
    {
        #region [-Properties-]
        public IEnumerable<Word> Words { get; set; } = new List<Word>();
        public string Comment { get; set; }
        public virtual string Key { get; set; }
        #endregion



        #region [-Constructor-]
        public GlobalWord(string comment, string key, params Word[] data) : this(key, comment)
        {
            foreach (var item in data)
            {
                Words.Append(item);
            }
        }
        public GlobalWord(string comment, string key)
        {
            Key = key;
            Comment = comment;
        }

        public GlobalWord()
        {
        }
        #endregion
    }

}
