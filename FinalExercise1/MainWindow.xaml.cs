using System;
using System.Collections.Generic;
using System.IO;
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
using FinalProjectBase;
using Microsoft.Win32;

namespace FinalExercise1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private string[] OpenedFileRaw;
        private Graph<int> _intUnweightedGraph;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenFileClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                OpenedFileRaw = File.ReadAllLines(openFileDialog.FileName);
            }

            ParseOpenedFile();
            
            MessageBox.Show("oten");
        }
        private void ParseOpenedFile()
        {
            _intUnweightedGraph = new Graph<int>(Convert.ToInt32(OpenedFileRaw[0]));
            for (int i = 0; i < Convert.ToInt32(OpenedFileRaw[0]); i++)
            {
                _intUnweightedGraph.AddVertex(i);
                
            }
            for (int j = 1; j < OpenedFileRaw.Length; j++)
            {
                string[] line = OpenedFileRaw[j].Split(' ');
                int currentVertex = Convert.ToInt32(line[0]);
                for (int i = 1; i < line.Length; i++)
                {
                    _intUnweightedGraph.Vertices[currentVertex].AddNeighbor(_intUnweightedGraph.Vertices[Convert.ToInt32(line[i])]);
                }

            }
        }

        private void DFSClick(object sender, RoutedEventArgs e)
        {
            var DFSResult = _intUnweightedGraph.DepthFirstSearch(Convert.ToInt32(DFSInitialIndexBox.Text));
            MessageBox.Show("Done!");
        }
    }
}
