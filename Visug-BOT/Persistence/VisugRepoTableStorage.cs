using System;
using System.Diagnostics;
using System.Threading.Tasks;

using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Visug2CommitBOTApp.Model;


namespace Visug2CommitBOTApp.Persistence
{
    public static class VisugRepoTableStorage<T> where T : class
    {
        private static CloudStorageAccount _cloudStorageAccount;
        private static CloudTableClient _cloudTableClient;

        private static string _reference;
        private static CloudTable _table;

        public static void Initialize(String tableReference)
        {
            _reference = tableReference;
            _cloudStorageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

            try
            {
                _cloudTableClient = _cloudStorageAccount.CreateCloudTableClient();
            }
            catch (Exception e)
            {
                Debug.Write("Could not create table client. Exception: {0}", e.ToString());  
            }

            _table = _cloudTableClient.GetTableReference(_reference);
            _table.CreateIfNotExistsAsync();
        }

        public static async Task<TableResult> CreateItemAsync(ITableEntity item)
        {
            var insertOperation = TableOperation.Insert(item);

            return await _table.ExecuteAsync(insertOperation);
        }
    }
}