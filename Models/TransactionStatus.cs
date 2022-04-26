
using System.ComponentModel;

namespace TransUploaderWebService.Models
{
    public enum TransactionStatus
    {
        [Description("A")]
        Approved,
        [Description("R")]
        Failed,
        [Description("R")]
        Rejected,
        [Description("D")]
        Finished,
        [Description("D")]
        Done
    }
}
