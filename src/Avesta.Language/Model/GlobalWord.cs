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
        public IList<Word> Words { get; set; } = new List<Word>();
        public string Comment { get; set; }
        public virtual object Key { get; set; }
        #endregion



        #region [-Constructor-]
        public GlobalWord(object key, string comment, params Word[] data) : this(key, comment)
        {
            foreach (var item in data)
            {
                Words.Add(item);
            }
        }
        public GlobalWord(object key, string comment)
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
