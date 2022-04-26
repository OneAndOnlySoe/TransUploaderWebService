using System.Data;
using System.Xml;
using TransUploaderWebService.Models;
using TransUploaderWebService.Services.Interface;

namespace TransUploaderWebService.Services
{
    public class XMLDataProcessor : IDataProcessor
    {
        private readonly Stream fileStream;
        private readonly string connectionStr;
        public XMLDataProcessor(Stream _fileStream, IConfiguration configuration)
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
                XmlDocument doc = new XmlDocument();
                doc.Load(fileStream);
                foreach (XmlNode node in doc.SelectNodes("/Transactions/Transaction"))
                {
                    DataRow dr = dt.NewRow();
                    dr["TransactionId"] = node.Attributes["id"].Value;

                    dr["TranscationDate"] = Convert.ToDateTime(node["TransactionDate"].InnerText);
                    TransactionStatus status = (TransactionStatus)Enum.Parse(typeof(TransactionStatus), node["Status"].InnerText);
                    dr["TranscationStatus"] = DBService.GetEnumDescription(status);

                    dr["Amount"] = node["PaymentDetails"].ChildNodes[0].InnerText;
                    dr["CurrencyCode"] = node["PaymentDetails"].ChildNodes[1].InnerText;
                    dt.Rows.Add(dr);
                }
                DBService.saveTransactionData(dt, connectionStr);
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
