using Microsoft.AspNetCore.Mvc;
using TransUploaderWebService.Models;
using TransUploaderWebService.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TransUploaderWebService.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly string connectionStr;
        public TransactionsController(IConfiguration configuration)
        {
            connectionStr = configuration["ConnectionStrings:SQLDBConnection"];
        }

        // GET: <TransactionsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET <TransactionsController>/GetTransByCurrency/USD
        [HttpGet("GetTransByCurrency/{currency}")]
        public IEnumerable<TransactionData> GetTransByCurrency(string currency)
        {
            IList<TransactionData> list = DBService.GetTransactionsByCurrency(currency, connectionStr);
            return list;
        }

        // GET <TransactionsController>/GetTransByStatus/Approved
        [HttpGet("GetTransByStatus/{status}")]
        public IEnumerable<TransactionData> GetTransByStatus(string status)
        {
            IList<TransactionData> list = DBService.GetTransactionsByStatus(status, connectionStr);
            return list;
        }

        // GET <TransactionsController>/GetTransByDateRange/Approved
        [HttpGet("GetTransByDateRange/{strDate}/{endDate}")]
        public IEnumerable<TransactionData> GetTransByDateRange(string strDate,string endDate)
        {
            IList<TransactionData> list = DBService.GetTransactionsByDateRange(strDate, endDate,connectionStr);
            return list;
        }

        // GET <TransactionsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST <TransactionsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT <TransactionsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE <TransactionsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
