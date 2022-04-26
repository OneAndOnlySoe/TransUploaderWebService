using TransUploaderWebService.Services.Interface;

namespace TransUploaderWebService.Services
{
    public class DataProcessorFactory : IDataProcessorFactory
    {
        private IDataProcessor processor;
        private readonly IConfiguration configuration;

        public DataProcessorFactory(IConfiguration _configuration)
        {
            configuration = _configuration;
        }
        public IDataProcessor CreateDataProcessor(string fileType, Stream fileStream)
        {
            processor = fileType switch
            {
                ".csv" => new CSVDataProcessor(fileStream, configuration),
                ".xml" => new XMLDataProcessor(fileStream, configuration),
                _ => null,
            };
            return processor;

        }
    }
}
