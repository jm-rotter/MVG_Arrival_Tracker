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
    public partial class WisdomView : UserControl
    {
        public WisdomView()
        {
            InitializeComponent();
            DataContext = new WisdomViewModel();
        }
    }
}