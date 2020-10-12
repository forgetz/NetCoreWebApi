using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Helpers
{
    public class FileHelpers
    {
        public static async Task<string> FileUpload(int? id, IFormFile file)
        {
            if (file == null)
                return null;
            if (file.Length < 1)
                return null;

            string uploadPath = Path.Combine("Uploads");
            string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + file.FileName;
            if (id.HasValue && id.Value > 0)
            {
                uploadPath = Path.Combine("Uploads", id.Value.ToString());
                if (!Directory.Exists(uploadPath))
                    Directory.CreateDirectory(uploadPath);
            }

            string saveFullFileName = Path.Combine(uploadPath, fileName);
            using (var stream = new FileStream(saveFullFileName, FileMode.Create))
            {
                await file.CopyToAsync(stream);
                return fileName;
            }


        }

    }
}
