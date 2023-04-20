using MauiTestAPIConnection.Interfaces;
using MauiTestAPIConnection.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MauiTestAPIConnection.Services
{
    public class RestService : IRestService
    {
        HttpClient _client;
        JsonSerializerOptions _serializerOptions;

        public List<Pizza> Pizzas { get; private set; }

        public RestService()
        {
            _client = new HttpClient();
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        public async Task<List<Pizza>> RefreshDataAsync()
        {
            Pizzas = new List<Pizza>();

            Uri uri = new Uri(string.Format(Constants.RestUrl, "pizzas"));
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    Pizzas = JsonSerializer.Deserialize<List<Pizza>>(content, _serializerOptions);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return Pizzas;
        }

        public async Task SavePizzaAsync(Pizza item, bool isNewItem = false)
        {
            Uri uri = new Uri(string.Format(Constants.RestUrl, "pizza"));

            try
            {
                string json = JsonSerializer.Serialize<Pizza>(item, _serializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                if (isNewItem)
                    response = await _client.PostAsync(uri, content);
                else
                    response = await _client.PutAsync(uri, content);

                if (response.IsSuccessStatusCode)
                    Debug.WriteLine(@"\tPizza successfully saved.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }
    }
}
