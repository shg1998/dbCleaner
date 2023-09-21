namespace DBCleaner.Models
{
    internal class DataBaseInformationModel
    {
        public string DataBaseName { get; set; }
        public TimeSpanDataModel TimeSpanRemoveData { get; set; }
        public List<TableInformationModel> TableAndFields { get; set; }
    }
}
