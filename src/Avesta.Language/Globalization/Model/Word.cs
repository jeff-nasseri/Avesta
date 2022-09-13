using Avesta.Language.Globalization.Enum;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Language.Globalization.Model
{
    public class Word : Globalization, IComparable<Word>
    {

        #region [-Constructor-]
        public Word()
        {
            Guid = System.Guid.NewGuid().ToString();
        }
        public Word(LanguageShortName language, AnnotationContentType contentType) : this()
        {
            Language = language;
            ContentType = contentType;
        }
        #endregion



        #region [-Properties-]
        string Content { get; set; }
        public virtual void OnSetContent(string content) => Content = content;
        public virtual string Guid { get; private set; }
        public LanguageShortName Language { get; private set; }
        public AnnotationContentType ContentType { get; private set; }
        #endregion




        #region [-Methods-]
        public override string ToString()
        {
            return Content;
        }

        public int CompareTo(Word other)
        {
            throw new NotImplementedException();
        }
        #endregion
    }

}
