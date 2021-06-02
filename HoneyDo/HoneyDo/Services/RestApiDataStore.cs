using HoneyDo.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HoneyDo.Services
{
    public class RestApiDataStore: IDataStore<HoneyDoItem>
    {
        string baseAddress;
        string honeyDoItemsUrl;

        public void Initialize()
        {
            // The iOS simulator can connect to localhost. 
            // However, Android emulators must use the 10.0.2.2 special alias
            // to your host loopback interface.
            //baseAddress = Device.RuntimePlatform ==
            //    Device.Android ? "https://10.0.2.2:58920" : "https://localhost:58920";

            baseAddress = "https://honeydoservice.azurewebsites.net";
            honeyDoItemsUrl = baseAddress + "/api/honeydoitems/{0}";
        }

        public async Task<List<HoneyDoItem>> GetItemsAsync()
        {
            var honeydoItems = new List<HoneyDoItem>();
            var uri = new Uri(string.Format(honeyDoItemsUrl, string.Empty));
            var httpClient = new HttpClient();
            //var httpClient = new HttpClient(new System.Net.Http.HttpClientHandler());

            try
            {
                var response = await httpClient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    honeydoItems = JsonConvert.DeserializeObject<List<HoneyDoItem>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return honeydoItems;

        }

        public async Task<HoneyDoItem> GetItemAsync(Int32 id)
        {
            var honeydoItem = new HoneyDoItem();
            var uri = new Uri(string.Format(honeyDoItemsUrl, id));
            var httpClient = new HttpClient(new System.Net.Http.HttpClientHandler());

            try
            {
                var response = await httpClient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    honeydoItem = JsonConvert.DeserializeObject<HoneyDoItem>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return honeydoItem;
        }

        public async Task<int> AddItemAsync(HoneyDoItem item)
        {
            var uri = new Uri(string.Format(honeyDoItemsUrl, string.Empty));
            var httpClient = new HttpClient(new System.Net.Http.HttpClientHandler());
            int success = 0;

            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                response = await httpClient.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\tHoneyDoItem successfully saved.");
                    success = 1;
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return success;
        }

        public async Task<int> UpdateItemAsync(HoneyDoItem item)
        {
            var uri = new Uri(string.Format(honeyDoItemsUrl, item.Id));
            var httpClient = new HttpClient(new System.Net.Http.HttpClientHandler());
            int success = 0;

            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                response = await httpClient.PutAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\tHoneyDoItem successfully saved.");
                    success = 1;
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return success;
        }

        public async Task<int> DeleteItemAsync(Int32 id)
        {
            var uri = new Uri(string.Format(honeyDoItemsUrl, id));
            var httpClient = new HttpClient(new System.Net.Http.HttpClientHandler());
            int success = 0;

            try
            {
                var response = await httpClient.DeleteAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\tHoneyDoItem successfully deleted.");
                    success = 1;
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return success;
        }

    }
}
