using System.Data;
using System.Globalization;
using TransUploaderWebService.Models;
using TransUploaderWebService.Services.Interface;

namespace TransUploaderWebService.Services
{
    public class CSVDataProcessor : IDataProcessor
    {
        private readonly Stream fileStream;
        private readonly string connectionStr;
        public CSVDataProcessor(Stream _fileStream,IConfiguration configuration)
        {
            fileStream = _fileStream;
            connectionStr = configuration["ConnectionStrings:SQLDBConnection"];
        }

        public string ProcessRequest()
        {
            string result = null;
            try
            {
                DataTable dt = DBService.GetDataTable();
                using (var sr = new StreamReader(fileStream))
                {
                    while (!sr.EndOfStream)
                    {
                        var lineValues = sr.ReadLine().Split(',');
                        DataRow dr = dt.NewRow();

                        dr["TransactionId"] = lineValues[0];
                        dr["Amount"] = Convert.ToDecimal(lineValues[1]);
                        dr["CurrencyCode"] = lineValues[2];
                        dr["TranscationDate"] = DateTime.ParseExact(lineValues[3], "dd/MM/yyyy hh:mm:ss", CultureInfo.InvariantCulture);

                        TransactionStatus status = (TransactionStatus)Enum.Parse(typeof(TransactionStatus), lineValues[4]);
                        dr["TranscationStatus"] = DBService.GetEnumDescription(status);
                        dt.Rows.Add(dr);
                    }
                    DBService.saveTransactionData(dt, connectionStr);
                }
                result = "Data recorded successfully!!";

            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }
        
        public void Dispose()
        {
            fileStream.Dispose();
        }
    }
}
