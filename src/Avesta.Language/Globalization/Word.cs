using System;
using System.Collections.Generic;
using System.Linq;

namespace Avesta.Language.Globalization
{

    public class GlobalWord
    {
        public IEnumerable<Word> Words { get; set; } = new List<Word>();

        public GlobalWord(params Word[] data)
        {
            foreach (var item in data)
            {
                Words.Append(item);
            }
        }
    }


    public enum AnnotationContentType
    {
        Required,
        Display,
        TypeError

    }
    public enum LanguageShortName
    {
        US_EN,
        IR_FA
    }



    public abstract class Globalization
    {
    }
    public class Word : Globalization, IComparable<Word>
    {
        public Word()
        {
            Guid = System.Guid.NewGuid().ToString();
        }
        public Word(string content, LanguageShortName language, AnnotationContentType contentType) : this()
        {
            Content = content;
            Language = language;
            ContentType = contentType;
        }

        public string Guid { get; private set; }

        public string Content { get; set; }
        public LanguageShortName Language { get; set; }
        public AnnotationContentType ContentType { get; set; }


        public int CompareTo(Word other)
        {
            throw new NotImplementedException();
        }
    }

}
