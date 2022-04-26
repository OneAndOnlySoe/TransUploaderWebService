using System.ComponentModel.DataAnnotations;
using TransUploaderWebService.Validators;

namespace TransUploaderWebService.Models
{
    public class UploaderViewModel 
    {
        [Required(ErrorMessage = "Select a file to upload.")]
        [DataType(DataType.Upload)]
        [MaxFileSize(ErrorMessage = "Maximum file size exceeded.")]
        [AllowedFileFormat(ErrorMessage = "Unknown format.")]
        public IFormFile UploadFile { get; set; }
    }
}
