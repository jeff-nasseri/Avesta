using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Language
{
    public class StartUp
    {
        public static void LoadLanguages()
        {
            LanguageRepository repository = Lang.Repository;
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Language");
            var files = Directory.GetFiles(path).Where(f => f.EndsWith("lang")).ToList();
            foreach (var file in files)
                repository.Add(new LanguageInfo(file));
            repository[Storage.Constant.Language.DefaultLanguagePrefix].Load();
        }
    }
}
