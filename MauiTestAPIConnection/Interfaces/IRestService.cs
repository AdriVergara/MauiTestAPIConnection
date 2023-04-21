using MauiTestAPIConnection.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiTestAPIConnection.Interfaces
{
    public interface IRestService
    {
        Task<List<Pizza>> RefreshDataAsync();
        Task<Pizza> GetPizzaAsync(int id);
        Task SavePizzaAsync(Pizza newPizza, bool isNewItem = false, int id = 0);
        Task DeletePizzaAsync(int id);
    }
}
