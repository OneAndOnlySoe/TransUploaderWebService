using System.ComponentModel.DataAnnotations;

namespace TransUploaderWebService.Models
{
    public class TransactionData
    {
        public string Id { get; set; }
        public string payment { get; set; }
        public string Status { get; set; }
    }
}
