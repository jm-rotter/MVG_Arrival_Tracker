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
    public partial class TrainView : UserControl
    {
        private ScrollViewer? _scrollViewer;
        private double _scrollSpeed = 5.0;
        private DispatcherTimer _timer;

        public TrainView()
        {
            InitializeComponent();
            DataContext = new TrainViewModel();
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(49)
            };
            _timer.Tick += (s, e) => DoScroll();
        }

        public void ToggleScroll_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (_timer.IsEnabled)
            {
                _timer.Stop();
                if (sender is Button btn) btn.Content = "Start Scroll";
            }
            else
            {
                _timer.Start();
                if (sender is Button btn) btn.Content = "Pause Scroll";
            }
        }

        private void DoScroll()
        {
            if (_scrollViewer == null)
            {
                _scrollViewer = ArrivalList.GetVisualDescendants()
                    .OfType<ScrollViewer>()
                    .FirstOrDefault();
                return;
            }

            double currentOffset = _scrollViewer.Offset.Y;
            double maxOffset = _scrollViewer.Extent.Height - _scrollViewer.Viewport.Height;

            if (maxOffset > 0)
            {
                if (currentOffset >= maxOffset)
                {
                    _scrollViewer.Offset = new Vector(0, 0);
                    _timer.Stop();
                    ScrollButton.Content = "Start Scroll";
                }
                else
                {
                    _scrollViewer.Offset = new Vector(0, currentOffset + _scrollSpeed);
                }
            }
        }
    }
}