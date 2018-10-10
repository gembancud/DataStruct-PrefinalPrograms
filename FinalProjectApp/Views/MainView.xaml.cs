using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using FinalProjectApp.Models;
using FinalProjectBase;
using MahApps.Metro.Controls.Dialogs;
using MaterialDesignThemes.Wpf;

namespace FinalProjectApp.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : MetroWindow
    {
        private bool isAddingLocation = false;
        private bool isAddingRoads = false;
        public static Timer dragTimer;
        public Point CurrPoint;
        public Shape SelectedObject;
        public Graph<LocationVertexPair> AppGraph;

        private Vertex<LocationVertexPair> FromRoad;
        private Vertex<LocationVertexPair> ToRoad;

        DoubleCollection doubleCollection=new DoubleCollection(){2,2};


        public MainView()
        {
            InitializeComponent();
            AppGraph = new Graph<LocationVertexPair>();

            EnableDrag();
        }

        #region Drag Functionality

        private void EnableDrag()
        {
            AppCanvas.MouseDown += new MouseButtonEventHandler(StartDrag);
            AppCanvas.MouseUp += new MouseButtonEventHandler(EndDrag);

            dragTimer = new Timer();
            dragTimer.Interval = 5;
            dragTimer.Elapsed += new ElapsedEventHandler(WhileDragged);
            dragTimer.AutoReset = true;

        }

        private void WhileDragged(object sender, ElapsedEventArgs e)
        {
            if (SelectedObject == null) return;

            Application.Current.Dispatcher.Invoke((Action)delegate
            {
                CurrPoint = Mouse.GetPosition(AppCanvas);

                Canvas.SetTop(SelectedObject, CurrPoint.Y - 25);
                Canvas.SetLeft(SelectedObject, CurrPoint.X - 25);

                int index = AppCanvas.Children.IndexOf(SelectedObject);
                AppGraph.Vertices[index].Data.X = CurrPoint.X;
                AppGraph.Vertices[index].Data.Y = CurrPoint.Y;

                Renderer();
            });
        }

        private void EndDrag(object sender, MouseButtonEventArgs e)
        {
            dragTimer.Stop();
            SelectedObject = null;

        }

        private void StartDrag(object sender, MouseButtonEventArgs e)
        {
            CurrPoint = Mouse.GetPosition(AppCanvas);
            var x = VisualTreeHelper.HitTest(AppCanvas, CurrPoint);
            SelectedObject = x.VisualHit as Ellipse;
            if (SelectedObject == null) return;
            dragTimer.Start();
        }

        #endregion
        #region Add Locations

        private void LocationButtonClick(object sender, RoutedEventArgs e)
        {
            AppCanvas.MouseDown -= new MouseButtonEventHandler(AddRoadEvent);
            AppCanvas.MouseDown -= new MouseButtonEventHandler(StartDrag);
            AppCanvas.MouseUp -= new MouseButtonEventHandler(EndDrag);
            RoadButton.IsEnabled = false;

            //            Start
            if (isAddingLocation == false)
            {
                AppCanvas.MouseDown += new MouseButtonEventHandler(AddLocationEvent);
                LocationButton.Content = "Stop Adding Locations";
                isAddingLocation = true;


            }
            //            Stop
            else if (isAddingLocation == true)
            {
                AppCanvas.MouseDown -= new MouseButtonEventHandler(AddLocationEvent);
                LocationButton.Content = "Start Adding Locations";
                isAddingLocation = false;

                AppCanvas.MouseDown += new MouseButtonEventHandler(StartDrag);
                AppCanvas.MouseUp += new MouseButtonEventHandler(EndDrag);
                RoadButton.IsEnabled = true;
            }

        }

        private void AddLocationEvent(object sender, MouseButtonEventArgs e)
        {
            Point p = Mouse.GetPosition(AppCanvas);
            AskLocationDialog(p);
        }

        private async Task AskLocationDialog(Point p)
        {
            var result = await this.ShowInputAsync("Location", "Input Locaton Name");
            if (result == null) return;

            Ellipse newEllipse = new Ellipse();
            newEllipse.Fill = Brushes.Blue;
            newEllipse.Width = 50;
            newEllipse.Height = 50;

            Canvas.SetTop(newEllipse, p.Y - 25);
            Canvas.SetLeft(newEllipse, p.X - 25);

            AppCanvas.Children.Add(newEllipse);

            LocationVertexPair pair = new LocationVertexPair(new Vertex<string>(result.ToString()), p.X, p.Y, newEllipse);
            AppGraph.AddVertex(pair);
            Renderer();
        }

        #endregion



        private void RoadButtonClick(object sender, RoutedEventArgs e)
        {
            AppCanvas.MouseDown -= new MouseButtonEventHandler(AddLocationEvent);
            AppCanvas.MouseDown -= new MouseButtonEventHandler(StartDrag);
            AppCanvas.MouseUp -= new MouseButtonEventHandler(EndDrag);
            LocationButton.IsEnabled = false;

            //            Start
            if (isAddingRoads == false)
            {
                AppCanvas.MouseDown += new MouseButtonEventHandler(AddRoadEvent);
                RoadButton.Content = "Stop Adding Roads";
                isAddingRoads = true;

            }
            //            Stop
            else if (isAddingRoads == true)
            {
                AppCanvas.MouseDown -= new MouseButtonEventHandler(AddRoadEvent);
                RoadButton.Content = "Start Adding Roads";
                isAddingRoads = false;

                AppCanvas.MouseDown += new MouseButtonEventHandler(StartDrag);
                AppCanvas.MouseUp += new MouseButtonEventHandler(EndDrag);
                LocationButton.IsEnabled = true;

                FromRoad = null;
                ToRoad = null;
            }
        }



        private void AddRoadEvent(object sender, MouseButtonEventArgs e)
        {
            if (FromRoad == null)
            {
                CurrPoint = Mouse.GetPosition(AppCanvas);
                var x = VisualTreeHelper.HitTest(AppCanvas, CurrPoint);
                SelectedObject = x.VisualHit as Shape;
                if (SelectedObject == null) return;

                int index = AppCanvas.Children.IndexOf(SelectedObject);
                FromRoad = AppGraph.Vertices[index];

            }
            else if (ToRoad == null)
            {
                CurrPoint = Mouse.GetPosition(AppCanvas);
                var x = VisualTreeHelper.HitTest(AppCanvas, CurrPoint);
                SelectedObject = x.VisualHit as Shape;
                if (SelectedObject == null) return;

                int index = AppCanvas.Children.IndexOf(SelectedObject);
                ToRoad = AppGraph.Vertices[index];


                //Case: Same Vertex
                if (ToRoad == FromRoad)
                {
                    this.ShowMessageAsync("Error!", "A Location cannot make a road to itself");
                    FromRoad = null;
                    ToRoad = null;
                    return;
                }

                CreateNeighbors();
            }


        }

        private async Task CreateNeighbors()
        {
            foreach (Neighbor<LocationVertexPair> fromRoadNeighbor in FromRoad.Neighbors)
            {
                if (fromRoadNeighbor.Vertex == ToRoad)
                {
                    AppSnackBar.MessageQueue = new SnackbarMessageQueue();
                    var fms = AppSnackBar.MessageQueue;
                    Task.Factory.StartNew(() => fms.Enqueue("Road Already Exists!"));
                    

                    return;
                }
            }

            var result = await this.ShowInputAsync("Distance",
                $"Input distance between {FromRoad.Data.Location.Data} and {ToRoad.Data.Location.Data}");

            if (Double.TryParse(result.ToString(), out double dresult))
            {
                AppGraph.AddUndirectedNeighbor(FromRoad.ID, ToRoad.ID, dresult);
                AppSnackBar.MessageQueue = new SnackbarMessageQueue();
                var ms = AppSnackBar.MessageQueue;
                Task.Factory.StartNew(() => ms.Enqueue("Road Successfully Added!"));
                ToRoad = null;
                FromRoad = null;
                Renderer();
            }
            else
            {
                AppSnackBar.MessageQueue = new SnackbarMessageQueue();
                var ms = AppSnackBar.MessageQueue;
                Task.Factory.StartNew(() => ms.Enqueue("Invalid Weight Input!"));
                ToRoad = null;
                FromRoad = null;
            }

            


        }

        private void Renderer()
        {
            AppCanvas.Children.Clear();

            foreach (Vertex<LocationVertexPair> appGraphVertex in AppGraph.Vertices)
            {
                var tmpElement = appGraphVertex.Data.Element;
                Canvas.SetTop(tmpElement, appGraphVertex.Data.Y - 25);
                Canvas.SetLeft(tmpElement, appGraphVertex.Data.X - 25);
                AppCanvas.Children.Add(tmpElement);



            }
            //Draw Line
            foreach (Vertex<LocationVertexPair> appGraphVertex in AppGraph.Vertices)
            {
                foreach (Neighbor<LocationVertexPair> neighbor in appGraphVertex.Neighbors)
                {
                    //MainRoad
                    Line newLine = new Line();
                    newLine.X2 = appGraphVertex.Data.X;
                    newLine.Y2 = appGraphVertex.Data.Y;

                    newLine.X1 = neighbor.Vertex.Data.X;
                    newLine.Y1 = neighbor.Vertex.Data.Y;

                    newLine.Stroke = Brushes.Black;
                    newLine.StrokeThickness = 40;

                    AppCanvas.Children.Add(newLine);


                    //DashLine
                    newLine = new Line();
                    newLine.X2 = appGraphVertex.Data.X;
                    newLine.Y2 = appGraphVertex.Data.Y;

                    newLine.X1 = neighbor.Vertex.Data.X;
                    newLine.Y1 = neighbor.Vertex.Data.Y;

                    newLine.Stroke = Brushes.White;
                    newLine.StrokeThickness = 5;
                    newLine.StrokeDashArray = doubleCollection;

                    AppCanvas.Children.Add(newLine);

                }
            }


        }
    }
}
