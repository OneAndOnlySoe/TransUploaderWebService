using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace TransUploaderWebService.Validators
{
    public class AllowedFileFormatAttribute : ValidationAttribute
    {
        private readonly IList<string> fileFormats;

        public AllowedFileFormatAttribute()
        {
            // allow file formats to upload are csv and xml.
            fileFormats = new List<string>{".csv", ".xml"};
        }

        public override bool IsValid(object? value)
        {
            if (value == null) { return false; }

            IFormFile file = value as IFormFile;
            string extension = Path.GetExtension(file.FileName);
            return fileFormats.Contains(extension);
        }
    }
}
