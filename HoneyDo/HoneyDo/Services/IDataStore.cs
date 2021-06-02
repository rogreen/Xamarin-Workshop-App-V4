using System.Collections.Generic;
using System.Threading.Tasks;

namespace HoneyDo.Services
{
    public interface IDataStore<T>
    {
        void Initialize();
        Task<List<T>> GetItemsAsync();
        Task<T> GetItemAsync(int id);
        Task<int> AddItemAsync(T item);
        Task<int> UpdateItemAsync(T item);
        Task<int> DeleteItemAsync(int id);
    }
}
