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
        public Word()
        {
            Guid = System.Guid.NewGuid().ToString();
        }
        public Word(LanguageShortName language, AnnotationContentType contentType) : this()
        {
            Language = language;
            ContentType = contentType;
        }

        public virtual string Guid { get; private set; }


        public string Content { get; private set; }
        public virtual void OnSetContent(string content) => Content = content;

        public LanguageShortName Language { get; private set; }
        public AnnotationContentType ContentType { get; private set; }


        public int CompareTo(Word other)
        {
            throw new NotImplementedException();
        }
    }

}
