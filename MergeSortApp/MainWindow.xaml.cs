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
            int i = 0;

            //Checks if all members are numbers
            bool allAreNumber = true;
            for (int j = 0; j < sample.Count; j++)
            {
                if (!int.TryParse(sample[i], out int num)) allAreNumber = false;
            }
            //If all are numbers, convert
            int[] sampleintarray = new int[sample.Count];
            if (allAreNumber)
            {
                for (int j = 0; j < sample.Count; j++)
                {
                    sampleintarray[j] = Convert.ToInt32(sample[j]);
                }

                var tempresult = MergeSort<int>.Do(sampleintarray);
                Result = new string[sampleintarray.Length];
                var tempHistory = MergeSort<int>.History;
                for (int j = 0; j < tempresult.Length; j++)
                {
                    Result[j] = tempresult[j].ToString();
                }
                History.Clear();
                foreach (int[] ints in tempHistory)
                {
                    string[] tempstep = new string[ints.Length];
                    for (int j = 0; j < ints.Length; j++)
                    {
                        tempstep[j] = ints[j].ToString();
                    }

                    History.Add(tempstep);
                }

            }
            else
            {
                Result = MergeSort<string>.Do(sample.ToArray());
                History = MergeSort<string>.History;
            }

            sample = Result.ToList();
            Array.ItemsSource = sample;
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
                        newTile.MaxWidth =100;
                        newTile.MaxHeight = 100;
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
            var stepNumber = flipview.SelectedIndex + 1;
            FlipView.BannerText = "Step: " + stepNumber;
        }
    }
}
