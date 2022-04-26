using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using Serilog;
using TransUploaderWebService.Models;

namespace TransUploaderWebService.Services
{
    public class DBService
    {
        public static DataTable GetDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[5] { 
                new DataColumn("TransactionId", typeof(string)),
                new DataColumn("Amount", typeof(decimal)),
                new DataColumn("CurrencyCode", typeof(string)),
                new DataColumn("TranscationStatus",typeof(string)),
                new DataColumn("TranscationDate",typeof(DateTime))
            });

            return dt;
        }
        public static void saveTransactionData(DataTable dataTable,string conStr)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(conStr))
                {
                    SqlBulkCopy bulkCopy = new SqlBulkCopy(connection);
                    //assigning Destination table name    
                    bulkCopy.DestinationTableName = "tbl_Transaction";
                    //Mapping Table column    
                    bulkCopy.ColumnMappings.Add("TransactionId", "TransactionId");
                    bulkCopy.ColumnMappings.Add("Amount", "Amount");
                    bulkCopy.ColumnMappings.Add("CurrencyCode", "CurrencyCode");
                    bulkCopy.ColumnMappings.Add("TranscationStatus", "TranscationStatus");
                    bulkCopy.ColumnMappings.Add("TranscationDate", "TranscationDate");

                    connection.Open();

                    bulkCopy.WriteToServer(dataTable);
                }
            }
            catch (SqlException ex)
            {
                Log.Error($"SaveTransactionData SqlException detail {ex.StackTrace}");
            }
            catch (Exception ex)
            {
                Log.Error($"SaveTransactionData Exception detail {ex.StackTrace}");
            }

            Log.Information("\nSaveTransactionData process done.");
        }
        public static String GetEnumDescription(Enum e)
        {
            FieldInfo fieldInfo = e.GetType().GetField(e.ToString());

            DescriptionAttribute[] enumAttributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (enumAttributes.Length > 0)
            {
                return enumAttributes[0].Description;
            }
            return e.ToString();
        }
        public static IList<TransactionData> GetTransactionsByCurrency(string currency, string conStr)
        {
            IList<TransactionData> transactions = new List<TransactionData>();
            try
            {
                using (SqlConnection connection = new SqlConnection(conStr))
                {
                    
                    using (SqlCommand cmd = new SqlCommand("select * from tbl_Transaction where CurrencyCode = @currency", connection))
                    {
                        cmd.Parameters.AddWithValue("@currency", currency);
                        connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                TransactionData transactionData = new TransactionData();
                                transactionData.Id = reader.GetString(0);
                                transactionData.payment = reader.GetDecimal(1) + " " + reader.GetString(2);
                                transactionData.Status = reader.GetString(3);
                                transactions.Add(transactionData);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error($"GetTransactionsByCurrency Exception detail {ex.StackTrace}");
            }

            return transactions;
        }
        public static IList<TransactionData> GetTransactionsByStatus(string status, string conStr)
        {
            IList<TransactionData> transactions = new List<TransactionData>();
            TransactionStatus enumStatus = (TransactionStatus)Enum.Parse(typeof(TransactionStatus), status);
            try
            {
                using (SqlConnection connection = new SqlConnection(conStr))
                {
                    using (SqlCommand cmd = new SqlCommand("select * from tbl_Transaction where TranscationStatus = @status", connection))
                    {
                        cmd.Parameters.AddWithValue("@status", GetEnumDescription(enumStatus));
                        connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                TransactionData transactionData = new TransactionData();
                                transactionData.Id = reader.GetString(0);
                                transactionData.payment = reader.GetDecimal(1) + " " + reader.GetString(2);
                                transactionData.Status = reader.GetString(3);
                                transactions.Add(transactionData);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error($"GetTransactionsByCurrency Exception detail {ex.StackTrace}");
            }

            return transactions;
        }
        public static IList<TransactionData> GetTransactionsByDateRange(string strDate, string endDate, string conStr)
        {
            IList<TransactionData> transactions = new List<TransactionData>();
            try
            {
                using (SqlConnection connection = new SqlConnection(conStr))
                {

                    using (SqlCommand cmd = new SqlCommand("select * from tbl_Transaction where TranscationDate between @strDate and @endDate", connection))
                    {
                        cmd.Parameters.AddWithValue("@strDate", strDate);
                        cmd.Parameters.AddWithValue("@endDate", endDate);
                        connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                TransactionData transactionData = new TransactionData();
                                transactionData.Id = reader.GetString(0);
                                transactionData.payment = reader.GetDecimal(1) + " " + reader.GetString(2);
                                transactionData.Status = reader.GetString(3);
                                transactions.Add(transactionData);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error($"GetTransactionsByCurrency Exception detail {ex.StackTrace}");
            }

            return transactions;
        }

    }
}
