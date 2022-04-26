namespace TransUploaderWebService.Services.Interface
{
    public interface IDataProcessorFactory
    {
        public IDataProcessor CreateDataProcessor(string fileType,Stream fileStream);
    }
}
