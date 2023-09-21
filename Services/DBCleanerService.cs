using System.Data.SqlClient;
using DBCleaner.Helpers;
using DBCleaner.Models;

namespace DBCleaner.Services
{
    internal class DBCleanerService
    {
        static DBCleanerService _instance;
        private static readonly object ObjLock = new object();
        private SqlConnection DataBasesSqlConnection;

        public static DBCleanerService Instance
        {
            get
            {
                lock (ObjLock)
                {
                    _instance ??= new DBCleanerService();
                }
                return _instance;
            }
        }

        public void Start(ConfigurationsModel configurationsModel)
        {
            var sleepTime = (int)UtilitiesHelper.ConvertTimeSpanDataModelToTimeSpan(configurationsModel.Frequncy).TotalMilliseconds;
            while (true)
            {
                try
                {
                    while (true)
                    {
                        Console.WriteLine($"Sleeping for {sleepTime} MilliSeconds");
                        Thread.Sleep(sleepTime);
                        this.InitSqlConnection(configurationsModel.ConnectionString);
                        this.DataBasesSqlConnection.Open();
                        foreach (var dataBaseInfo in configurationsModel.DataBaseInformationModels)
                        {
                            this.DataBasesSqlConnection.ChangeDatabase(dataBaseInfo.DataBaseName);
                            var remainTime = DateTime.Now.Ticks - UtilitiesHelper.ConvertTimeSpanDataModelToTimeSpan(dataBaseInfo.TimeSpanRemoveData).Ticks;
                            var stringCommand = dataBaseInfo.TableAndFields.Aggregate("", (current, tableInfo) => current + ("DELETE FROM " + tableInfo.TableName + " WHERE " + tableInfo.FieldName + " < '" + remainTime + "';"));
                            using var command = new SqlCommand(stringCommand, this.DataBasesSqlConnection);
                            command.ExecuteNonQuery();
                        }
                        this.DataBasesSqlConnection.Close();
                        Console.WriteLine($"Information => Deleted");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error Occurred => {e.Message}");
                }
            }
        }

        private void InitSqlConnection(string connectionString)
        {
            this.DataBasesSqlConnection?.Close();
            this.DataBasesSqlConnection?.Dispose();
            this.DataBasesSqlConnection = new SqlConnection(connectionString);
        }
    }
}
