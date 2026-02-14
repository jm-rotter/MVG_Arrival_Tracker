using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using TrainApp.Models;
using TrainApp.Services;
using Avalonia.Threading;

namespace TrainApp.ViewModels
{

    public partial class TrainViewModel : ObservableObject
    {
        private readonly MVG_Services _mvgService = new();

        [ObservableProperty]
        private ObservableCollection<Departure> _departures = new();

        public TrainViewModel()
        {
            _ = LoadDeparturesAsync();
        }

        private async Task LoadDeparturesAsync()
        {

            while (true)
            {
                var results = await _mvgService.GetDeparturesAsync();
                results = _mvgService.filterDepartures(results); 
            
                Dispatcher.UIThread.Post(() =>
                {
                    Departures.Clear();
                    foreach (var d in results)
                    {
                        Departures.Add(d);
                    }
                });

                await Task.Delay(30000);
            }
        }
    }
}
