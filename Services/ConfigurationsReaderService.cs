using System.Reflection;
using DBCleaner.Models;
using Newtonsoft.Json;

namespace DBCleaner.Services
{
    internal class ConfigurationsReaderService
    {
        private static ConfigurationsReaderService _instance;
        private static readonly object ObjLock = new object();
        private ConfigurationsModel _configurationsModel;

        public static ConfigurationsReaderService Instance
        {
            get
            {
                lock (ObjLock) _instance ??= new ConfigurationsReaderService();
                return _instance;
            }
        }

        private ConfigurationsReaderService()
        {
            this.ReadConfigurationsFromFile();
        }

        private void ReadConfigurationsFromFile()
        {
            //to working on Host
            //using var r = new StreamReader(Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location) + "\\..\\" + "/DataBasesInformation.json");

            //to working on Docker Container
            using var r = new StreamReader(Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location) + "/DataBasesInformation.json");
            var json = r.ReadToEnd();
            this._configurationsModel = JsonConvert.DeserializeObject<ConfigurationsModel>(json);
        }

        public ConfigurationsModel GetConfigurations() => this._configurationsModel;
    }
}
