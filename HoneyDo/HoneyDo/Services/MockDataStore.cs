using HoneyDo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HoneyDo.Services
{
    public class MockDataStore : IDataStore<HoneyDoItem>
    {
        List<HoneyDoItem> honeyDoItems;

        public void Initialize()
        {
            honeyDoItems = new List<HoneyDoItem>()
        {
            new HoneyDoItem
            {
                Id = 1,
                Description = "Clean litter box",
                AssignedTo="Me",
                Priority="High",
                Recurrence="Monthly",
                Category="Home indoors",
                DueDate=DateTime.Today.AddDays(-2)
            },
            new HoneyDoItem
            {
                Id = 2,
                Description = "Grocery shop",
                AssignedTo="You",
                Priority="High",
                Recurrence="Weekly",
                Category="Errand",
                DueDate=DateTime.Today.AddDays(2)
            },
            new HoneyDoItem
            {
                Id = 3,
                Description = "Book vacation travel",
                AssignedTo="You",
                Priority="Low",
                Recurrence="None",
                Category="Home indoors",
                DueDate=DateTime.Today.AddDays(2)
            },
            new HoneyDoItem
            {
                Id = 4,
                Description = "Mow the lawn",
                AssignedTo="Me",
                Priority="Medium",
                Recurrence="Weekly",
                Category="Home outdoors",
                DueDate=DateTime.Today.AddDays(5)
            },
            new HoneyDoItem
            {
                Id = 5,
                Description = "Dust and vacuum",
                AssignedTo="Us",
                Priority="Medium",
                Recurrence="Monthly",
                Category="Home indoors",
                DueDate=DateTime.Today.AddDays(7)
            }
            };
        }

        public async Task<List<HoneyDoItem>> GetItemsAsync()
        {
            return await Task.FromResult(honeyDoItems);
        }

        public async Task<HoneyDoItem> GetItemAsync(int id)
        {
            return await Task.FromResult(honeyDoItems.FirstOrDefault(s => s.Id == id));
        }

        public async Task<int> AddItemAsync(HoneyDoItem honeyDoItem)
        {
            honeyDoItems.Add(honeyDoItem);

            return await Task.FromResult(1);
        }

        public async Task<int> UpdateItemAsync(HoneyDoItem honeyDoItem)
        {
            var oldHoneyDoItem = honeyDoItems.Where(
                (HoneyDoItem arg) => arg.Id == honeyDoItem.Id).FirstOrDefault();
            honeyDoItems.Remove(oldHoneyDoItem);
            honeyDoItems.Add(honeyDoItem);

            return await Task.FromResult(1);
        }

        public async Task<int> DeleteItemAsync(int id)
        {
            var oldHoneyDoItem = honeyDoItems.Where(
                (HoneyDoItem arg) => arg.Id == id).FirstOrDefault();
            honeyDoItems.Remove(oldHoneyDoItem);

            return await Task.FromResult(1);
        }

    }
}
