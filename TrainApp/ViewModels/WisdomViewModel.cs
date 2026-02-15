using System.Collections.ObjectModel;
using Avalonia.Threading;
using TrainApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Threading.Tasks;
using System;


namespace TrainApp.ViewModels
{
    public partial class WisdomViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _wisdomText = "Loading Wisdom";

        private readonly WisdomService _wisdomService = new();

        public WisdomViewModel()
        {
            _ = LoadWisdomLoopAsync();
        }

        private async Task LoadWisdomLoopAsync()
        {
            while(true)
            {
                var newWisdom = await _wisdomService.GetRandomWisdomAsync();
                Dispatcher.UIThread.Post(() => WisdomText = newWisdom);
                await Task.Delay(TimeSpan.FromMinutes(1.5));
            }
        }

    }
}