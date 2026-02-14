using Avalonia;
using Avalonia.Controls;
using Avalonia.Threading;     
using Avalonia.VisualTree;    
using Avalonia.LogicalTree;
using System;
using System.Linq;            
using TrainApp.ViewModels;

namespace TrainApp.Views
{
    public partial class WeatherView : UserControl
    {
        public WeatherView()
        {
            InitializeComponent();
            DataContext = new WeatherViewModel();
        }
    }
}