namespace DBCleaner.Models
{
    internal class ConfigurationsModel
    {
        public string ConnectionString { get; set; }
        public TimeSpanDataModel Frequncy { get; set; }
        public List<DataBaseInformationModel> DataBaseInformationModels { get; set; }
    }
}
