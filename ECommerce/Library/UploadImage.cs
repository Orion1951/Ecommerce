using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Library
{
   public class UploadImage
   {
      public async Task<byte[]> ByteAvatarImageAsync(IFormFile AvatarImage, IWebHostEnvironment environment)
      {
         string image = "images/images/default.png";
         if (AvatarImage != null)
         {
            using var memoryStream = new MemoryStream();
            return memoryStream.ToArray();
         }
         else
         {
            var archivoOrigen = $"{environment.ContentRootPath}/wwwroot/{image}";
            return File.ReadAllBytes(archivoOrigen);
         }
      }

   }
}
