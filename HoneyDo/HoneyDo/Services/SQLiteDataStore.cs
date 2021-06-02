using HoneyDo.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace HoneyDo.Services
{
    public class SQLiteDataStore : IDataStore<HoneyDoItem>
    {
        SQLiteAsyncConnection database;

        public void Initialize()
        {
            database = new SQLiteAsyncConnection(
                Path.Combine(Environment.GetFolderPath(
                    Environment.SpecialFolder.LocalApplicationData),
                    "HoneyDoSQLite.db3"));
            database.CreateTableAsync<HoneyDoItem>().Wait();
        }

        public Task<List<HoneyDoItem>> GetItemsAsync()
        {
            //return database.Table<HoneyDoItem>().ToListAsync();
            return database.QueryAsync<HoneyDoItem>(
                "SELECT * FROM [HoneyDoItem] WHERE [Completed] = 0 " +
                "ORDER BY [DueDate]");
        }

        public Task<HoneyDoItem> GetItemAsync(Int32 id)
        {
            return database.Table<HoneyDoItem>()
                .Where(i => i.Id == id).FirstOrDefaultAsync();
        }


        public Task<int> AddItemAsync(HoneyDoItem item)
        {
            return database.InsertAsync(item);
        }

        public Task<int> UpdateItemAsync(HoneyDoItem item)
        {
            return database.UpdateAsync(item);
        }

        public Task<int> DeleteItemAsync(Int32 id)
        {
            var item = database.Table<HoneyDoItem>()
                .Where(i => i.Id == id).FirstOrDefaultAsync();

            return database.DeleteAsync(item);
        }

    }
}
