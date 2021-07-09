using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace GymTool.Library
{
    public class Uploadimage
    {
        public async Task<byte[]> ByteAvatarImageAsync(IFormFile AvatarImage, IWebHostEnvironment environment, String imagen)
        {

            if (AvatarImage != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await AvatarImage.CopyToAsync(memoryStream);
                    return memoryStream.ToArray();
                }

            }
            else
            {
                var archivoOrigen = $"{environment.ContentRootPath}/wwwroot/{imagen}";
                return File.ReadAllBytes(archivoOrigen);
            }
        }
    }
}
