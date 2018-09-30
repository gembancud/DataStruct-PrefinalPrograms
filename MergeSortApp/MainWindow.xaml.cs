using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using MergeSortBase;

namespace MergeSortApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public List<string> sample { get; set; }
        public String[] Result { get; set; }
        public List<String[]> History { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            sample = new List<string>() { "Farah", "Ben", "Alex", "Cathy", "Echo", "Dingdong", "Amanda" };
            Array.ItemsSource = sample;
        }

        private void AddItemClick(object sender, RoutedEventArgs e)
        {
            if (NewItemBox.Text == "") return;
            sample.Add(NewItemBox.Text);
            NewItemBox.Text = null;
            Array.Items.Refresh();
        }

        private void SortArrayClick(object sender, RoutedEventArgs e)
        {
            FlipView.Items.Clear();
            
            Result = MergeSort<string>.Do(sample.ToArray());
            sample = Result.ToList();
            Array.ItemsSource = sample;
            History = MergeSort<string>.History;
            var x = CreateFlipViews();
            foreach (FlipViewItem flipViewItem in x)
            {
                FlipView.Items.Add(flipViewItem);
            }

            FlipView.SelectedIndex = 0;

            yourMahAppFlyout.IsOpen = false;
            
        }

        private void DeleteItemClick(object sender, RoutedEventArgs e)
        {
            try
            {
                sample.Remove(Array.SelectedItems[0].ToString());
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
               
            }
            Array.Items.Refresh();
        }

        private List<FlipViewItem> CreateFlipViews()
        {
            List<FlipViewItem> tempresult = new List<FlipViewItem>();
                foreach (string[] hist in History)
                {
                    WrapPanel stackPanel = new WrapPanel();
                    stackPanel.Orientation = Orientation.Horizontal;
                    foreach (string s in hist)
                    {
                        Tile newTile= new Tile();
                        newTile.Title = s;
                        newTile.VerticalTitleAlignment = VerticalAlignment.Center;
                        newTile.TitleFontSize = 24;
                        newTile.MaxHeight = 85;
                        newTile.MaxWidth = 85;
                        stackPanel.Children.Add(newTile);
                    }

                    Grid newGrid = new Grid();
                    newGrid.Children.Add(stackPanel);
                    stackPanel.VerticalAlignment = VerticalAlignment.Center;
                    stackPanel.HorizontalAlignment = HorizontalAlignment.Center;
                    FlipViewItem item = new FlipViewItem();
                    item.Content = newGrid;
                    tempresult.Add(item);
                }
            return tempresult;
        }

        private void FlipViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var flipview = ((FlipView)sender);
            FlipView.BannerText = "Step: " + flipview.SelectedIndex;
        }
    }
}
