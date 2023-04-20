using MauiTestAPIConnection.Interfaces;
using MauiTestAPIConnection.Models;
using MauiTestAPIConnection.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiTestAPIConnection.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string _helloVar;

        public string HelloVar
        {
            get => _helloVar;
            set
            {
                if (_helloVar != value)
                {
                    _helloVar = value;
                    OnPropertyChanged(); // reports this property
                }
            }
        }

        private List<Pizza> _pizzas;

        public List<Pizza> Pizzas
        {
            get => _pizzas;
            set
            {
                if (_pizzas != value)
                {
                    _pizzas = value;
                    OnPropertyChanged(); // reports this property
                }
            }
        }

        private Pizza _newPizza;
         
        public Pizza NewPizza
        {
            get => _newPizza;
            set
            {
                if (_newPizza != value)
                {
                    _newPizza = value;
                    OnPropertyChanged(); // reports this property
                }
            }
        }

        public ICommand SavePizza { set; get; }

        public void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        private IRestService _restService;

        private readonly Task getPizzasAsyncTask;

        public MainPageViewModel(IRestService restService)
        {
            _restService = restService;

            SavePizza = new Command(async () => await ExecuteSavePizzaAsync());

            NewPizza = new Pizza();

            //Not recommended
            //var t = Task.Run(() => GetPizzasAsync());
            //t.Wait();
            //Pizzas = t.Result;

            getPizzasAsyncTask = GetPizzasAsync();
        }

        private async Task GetPizzasAsync()
        {
            Pizzas = await _restService.RefreshDataAsync();
        }

        private async Task ExecuteSavePizzaAsync()
        {
            await _restService.SavePizzaAsync(NewPizza, true);
            await GetPizzasAsync();
        }
    }
}
