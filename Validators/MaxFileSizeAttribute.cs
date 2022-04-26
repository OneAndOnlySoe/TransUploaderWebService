using System.ComponentModel.DataAnnotations;

namespace TransUploaderWebService.Validators
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int maxFileSize;

        public MaxFileSizeAttribute()
        {
            // max file size 1 MB
            maxFileSize = 1 * 1024 * 1024;
        }
        public override bool IsValid(object? value)
        {
            if (value == null) { return false; }

            IFormFile file = value as IFormFile;
            return file.Length <= maxFileSize;
        }
    }
}
