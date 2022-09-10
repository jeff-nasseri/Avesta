using Avesta.Globalization.Language;
using System.Threading.Tasks;

namespace Avesta.Language.Globalization
{
    public abstract class WordContext
    {
        public async virtual Task OnCreate(LangContextProvider provider)
        {
        }
    }









    public class AvestaWordContext : WordContext
    {
        public static GlobalWord Hello = new GlobalWord(
            new Word("Hello", LanguageShortName.US_EN, AnnotationContentType.Display)
            , new Word("سلام", LanguageShortName.IR_FA, AnnotationContentType.Display)
            );
    }



}
