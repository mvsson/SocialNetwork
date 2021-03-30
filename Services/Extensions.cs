using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SocialNetwork.Services
{
    public static class Extensions
    {
        public static bool IsPicExtention(this IFormFile file)
        {
            string[] permittedExtensions = { ".jpeg", ".jpg", ".gif", ".png" };

            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (string.IsNullOrEmpty(ext) || !permittedExtensions.Contains(ext))
            {
                return false;
            }
            return true;
        }

        public static bool IsValidSize(this IFormFile file, long byteLenght)
        {
            if (file.Length > byteLenght)
                return false;
            return true;
        }
    }
}
