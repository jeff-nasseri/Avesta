using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Share.Helper
{
    public class FileHelper
    {
        public static async Task Save(IFormFile file, string path)
        {
            if (file == null || file.Length == 0)
                return;

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
        }

   

    }
}
