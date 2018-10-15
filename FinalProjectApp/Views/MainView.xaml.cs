using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup.Localizer;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using FinalProjectApp.Models;
using FinalProjectBase;
using MahApps.Metro.Controls.Dialogs;
using MaterialDesignThemes.Wpf;
using FinalProjectApp.Properties;
using static System.Math;

namespace FinalProjectApp.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : MetroWindow
    {
        #region Fields and Properties
        private bool isAddingLocation = false;
        private bool isAddingRoads = false;
        private bool isSimulating = false;
        public static Timer dragTimer;
        public static Timer ClockTimer;
        public Point CurrPoint;
        public Shape SelectedObject;
        public Graph<LocationVertexPair> AppGraph;
        public IList<Vehicle> VehicleList;
        public double ClockTimerPeriod = 16;
        private Vertex<LocationVertexPair> FromRoad;
        private Vertex<LocationVertexPair> ToRoad;
        DoubleCollection doubleCollection = new DoubleCollection() { 6, 6 };
        public ObservableCollection<Vertex<LocationVertexPair>> LocationCollection
        {
            get
            {
                var tmpcollection = new ObservableCollection<Vertex<LocationVertexPair>>();
                foreach (var x in AppGraph.Vertices)
                {
                    tmpcollection.Add(x);
                }

                return tmpcollection;
            }
        }
        public ObservableCollection<Vehicle> VehicleCollection
        {
            get
            {
                var tmpcollection = new ObservableCollection<Vehicle>();
                foreach (var vehicle in VehicleList)
                {
                    tmpcollection.Add(vehicle);
                }

                return tmpcollection;
            }
        }
        private MediaPlayer mediaPlayer = new MediaPlayer();
        private bool isPlaying = false;
        private Random random = new Random();
        private SnackbarMessageQueue fms;


        #endregion



        public MainView()
        {
            InitializeComponent();
            AppGraph = new Graph<LocationVertexPair>();
            VehicleList = new List<Vehicle>();

            EnableDrag();
            SetClockTimer();
            PrepareMusic();
            PrepareSnackBar();
        }

        private void PrepareSnackBar()
        {
            fms = new SnackbarMessageQueue();
            AppSnackBar.MessageQueue = fms;
        }


        #region Constructor Operations
        private void SetClockTimer()
        {
            ClockTimer = new Timer();
            ClockTimer.Interval = ClockTimerPeriod;
            ClockTimer.Elapsed += new ElapsedEventHandler(SimulationManager);
            ClockTimer.AutoReset = true;
        }
        private void EnableDrag()
        {
            AppCanvas.MouseDown += new MouseButtonEventHandler(StartDrag);
            AppCanvas.MouseUp += new MouseButtonEventHandler(EndDrag);

            dragTimer = new Timer();
            dragTimer.Interval = 16;
            dragTimer.Elapsed += new ElapsedEventHandler(WhileDragged);
            dragTimer.AutoReset = true;

        }
        private void PrepareMusic()
        {
            int index = random.Next(1, 10);
            string musicpath = "Music" + index + ".mp3";
            mediaPlayer.Open(new Uri(@musicpath, UriKind.Relative));
        }
        #endregion

        #region Drag Functionality
        private void WhileDragged(object sender, ElapsedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke((Action)delegate
            {
                if (SelectedObject == null) return;
                CurrPoint = Mouse.GetPosition(AppCanvas);
                //                Canvas.SetTop(SelectedObject, CurrPoint.Y - 70);
                //                Canvas.SetLeft(SelectedObject, CurrPoint.X - 70);

                int index = AppCanvas.Children.IndexOf(SelectedObject);
                AppGraph.Vertices[index].Data.X = CurrPoint.X;
                AppGraph.Vertices[index].Data.Y = CurrPoint.Y;

                Renderer();
                RenderCars();
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
            StartButton.IsEnabled = false;

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
                StartButton.IsEnabled = true;

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
            if (String.IsNullOrWhiteSpace(result)) return;

            Ellipse newEllipse = new Ellipse();

            newEllipse.Width = 60;
            newEllipse.Height = 60;
            newEllipse.Fill = Brushes.Black;
            newEllipse.StrokeThickness = 4;
            newEllipse.Stroke = Brushes.Azure;

            //newEllipse.Fill = new ImageBrush(new BitmapImage(
            //    new Uri(@"Marker.png", UriKind.Relative)));

            //            Canvas.SetTop(newEllipse, p.Y - 30);
            //            Canvas.SetLeft(newEllipse, p.X - 50);

            AppCanvas.Children.Add(newEllipse);

            LocationVertexPair pair = new LocationVertexPair(new Vertex<string>(result.ToString()), p.X, p.Y, newEllipse);
            AppGraph.AddVertex(pair);
            Renderer();
        }
        #endregion

        #region Add Roads
        private void RoadButtonClick(object sender, RoutedEventArgs e)
        {
            AppCanvas.MouseDown -= new MouseButtonEventHandler(AddLocationEvent);
            AppCanvas.MouseDown -= new MouseButtonEventHandler(StartDrag);
            AppCanvas.MouseUp -= new MouseButtonEventHandler(EndDrag);
            LocationButton.IsEnabled = false;
            StartButton.IsEnabled = false;


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
                StartButton.IsEnabled = true;


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
                SelectedObject = x.VisualHit as Ellipse;
                if (SelectedObject == null) return;


                int index = AppCanvas.Children.IndexOf(SelectedObject);
                FromRoad = AppGraph.Vertices[index];
                DisplayToSnackBar($"{FromRoad.Data.Location.Data} was Selected");
            }
            else if (ToRoad == null)
            {
                CurrPoint = Mouse.GetPosition(AppCanvas);
                var x = VisualTreeHelper.HitTest(AppCanvas, CurrPoint);
                SelectedObject = x.VisualHit as Ellipse;
                if (SelectedObject == null) return;

                int index = AppCanvas.Children.IndexOf(SelectedObject);
                ToRoad = AppGraph.Vertices[index];
                DisplayToSnackBar($"{ToRoad.Data.Location.Data} was Selected");


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
                    DisplayToSnackBar("Road Already Exists!");


                    return;
                }
            }

            var result = await this.ShowInputAsync("Distance",
                $"Input distance between {FromRoad.Data.Location.Data} and {ToRoad.Data.Location.Data}");

            if (Double.TryParse(result.ToString(), out double dresult))
            {
                AppGraph.AddUndirectedNeighbor(FromRoad.ID, ToRoad.ID, dresult);
                DisplayToSnackBar("Road Successfully Added!");
                ToRoad = null;
                FromRoad = null;
                Renderer();
            }
            else
            {
                DisplayToSnackBar("Invalid Weight Input!");
                ToRoad = null;
                FromRoad = null;
            }




        }
        #endregion

        #region Create Car
        private void AddCarClick(object sender, RoutedEventArgs e)
        {
            AddCarFlyout.IsOpen = true;
            AddCarFrom.ItemsSource = LocationCollection;
            AddCarTo.ItemsSource = LocationCollection;
        }
        private void CreateCarButtonClick(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(AddCarName.Text) &&
                !String.IsNullOrWhiteSpace(CarDesignComboBox.Text) &&
                !String.IsNullOrWhiteSpace(AddCarFrom.Text) &&
                !String.IsNullOrWhiteSpace(AddCarTo.Text) &&
                !String.IsNullOrWhiteSpace(AddCarSpeed.Text) &&
                Double.TryParse(AddCarSpeed.Text, out double carSpeed)
            )
            {
                CreateCar(AddCarName.Text, AddCarFrom.SelectionBoxItem, AddCarTo.SelectionBoxItem, carSpeed);
            }
            else DisplayToSnackBar("Error in InputFields!");
        }
        private void CreateCar(string carName, object carFrom, object carTo, double carSpeed)
        {
            AddCarFlyout.IsOpen = false;
            var carFromVertex = carFrom as Vertex<LocationVertexPair>;
            var carToVertex = carTo as Vertex<LocationVertexPair>;

            //Assign visual representation
            Rectangle newRectangle = new Rectangle();
            newRectangle.Width = 60;
            newRectangle.Height = 30;
            string carpath = "Car" + CarDesignComboBox.SelectedIndex + ".png";
            newRectangle.Fill = new ImageBrush(new BitmapImage(
                new Uri(@carpath, UriKind.Relative)));

            //Creates Temporary Vehicle
            var tmpVehicle = new Vehicle(carName, carFromVertex, carToVertex, carSpeed, newRectangle);
            //Evaluates Vehicle Route
            tmpVehicle.Route = AppGraph.CalculateShortestRoutes(tmpVehicle.From.ID);
            tmpVehicle.TotalDistance = tmpVehicle.Route.GetDisplacementOfVertex(tmpVehicle.To);
            tmpVehicle.SetTravelRoute();
            if (tmpVehicle.Route.DisplacementList[carToVertex.ID] == Double.MaxValue)
            {
                DisplayToSnackBar("Car Not Added! Destination Unreachable");
                return;
            }

            //Car Chip
            Chip newChip = new Chip();
            var tmpChipName = tmpVehicle.Name;
            newChip.Content = tmpChipName;
            newChip.Foreground = Brushes.AliceBlue;
            newChip.RenderTransformOrigin = new Point(0.5, 0.5);
            tmpVehicle.Chip = newChip;

            //            //Presets Vehicle Set up to avoid Random Null Cases
            //            tmpVehicle.CurrLocation = tmpVehicle.From;
            //            tmpVehicle.CurrDestination = tmpVehicle.TravelRoute.Pop();
            //            tmpVehicle.X = tmpVehicle.From.Data.X;
            //            tmpVehicle.Y = tmpVehicle.From.Data.Y;


            VehicleList.Add(tmpVehicle);
            CarListView.ItemsSource = VehicleCollection;

            DisplayToSnackBar("Car Successfully Added!");
        }
        #endregion

        #region Remove car
        private void RemoveCarClick(object sender, RoutedEventArgs e)
        {
            var toReemove = CarListView.SelectedItem as Vehicle;
            VehicleList.Remove(toReemove);
            CarListView.ItemsSource = VehicleCollection;
            DisplayToSnackBar("Car was removed!");
        }
        #endregion

        #region Simulation
        private void StartTimeClick(object sender, RoutedEventArgs e)
        {
            RoadButton.IsEnabled = false;
            LocationButton.IsEnabled = false;
            //            Start
            if (isSimulating == false)
            {
                StartButton.Content = "Stop Simulation";
                isSimulating = true;
                //                ActivateAllVehicles();
                ClockTimer.Start();
            }
            //            Stop
            else if (isSimulating == true)
            {
                StartButton.Content = "Begin Simulation";
                isSimulating = false;

                RoadButton.IsEnabled = true;
                LocationButton.IsEnabled = true;
                ClockTimer.Stop();
            }
        }
        private void SimulationManager(object sender, ElapsedEventArgs e)
        {
            TimerDisplay();
            ComputeActiveCars();
            RenderCars();
        }
        #endregion

        #region Helpers
        private void Renderer()
        {
            this.Dispatcher.Invoke(() =>
            {
               AppCanvas.Children.Clear();
                //Draw Vertices
                foreach (Vertex<LocationVertexPair> appGraphVertex in AppGraph.Vertices)
                {
                    //Vertex
                    var tmpElement = appGraphVertex.Data.Element;
                    Canvas.SetTop(tmpElement, appGraphVertex.Data.Y - 30);
                    Canvas.SetLeft(tmpElement, appGraphVertex.Data.X - 30);
//                   AppCanvas.Children.Add(tmpElement);

                    int elementIndex = AppCanvas.Children.IndexOf(tmpElement);
                    if (elementIndex == -1)
                        AppCanvas.Children.Add(tmpElement);
                    else
                        AppCanvas.Children[elementIndex] = tmpElement;
                }

                //Draw Location Chips
                foreach (Vertex<LocationVertexPair> appGraphVertex in AppGraph.Vertices)
                {
                    //Vertex Name
                    Chip newChip = new Chip();
                    var tmpChipName = appGraphVertex.Data.Location.Data;
                    newChip.Content = tmpChipName;
                    newChip.Foreground = Brushes.AliceBlue;
                    newChip.RenderTransformOrigin = new Point(0.5, 0.5);

                    Canvas.SetTop(newChip, appGraphVertex.Data.Y + 20);
                    Canvas.SetLeft(newChip, appGraphVertex.Data.X - 25);
                    //                    AppCanvas.Children.Add(newChip);

                    int chipIndex = AppCanvas.Children.IndexOf(newChip);
                    if (chipIndex == -1)
                        AppCanvas.Children.Add(newChip);
                    else
                        AppCanvas.Children[chipIndex] = newChip;
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
                        newLine.StrokeThickness = 30;
                        newLine.StrokeEndLineCap = PenLineCap.Round;
                        newLine.StrokeStartLineCap = PenLineCap.Round;

                        //                        AppCanvas.Children.Add(newLine);
                        int lineIndex = AppCanvas.Children.IndexOf(newLine);
                        if (lineIndex == -1)
                            AppCanvas.Children.Add(newLine);
                        else
                            AppCanvas.Children[lineIndex] = newLine;


                        //RoadLength Chip
                        Chip newChip = new Chip();
                        newChip.Content = neighbor.Weight;
                        newChip.Foreground = Brushes.LightCyan;
                        newChip.RenderTransformOrigin = new Point(0.5, 0.5);

                        var deltaY = appGraphVertex.Data.Y - neighbor.Vertex.Data.Y;
                        var deltaX = appGraphVertex.Data.X - neighbor.Vertex.Data.X;

                        Canvas.SetTop(newChip, neighbor.Vertex.Data.Y + (0.5 * deltaY) - 40);
                        Canvas.SetLeft(newChip, neighbor.Vertex.Data.X + (0.5 * deltaX) - 40);

                        //                        AppCanvas.Children.Add(newChip);
                        int chipIndex = AppCanvas.Children.IndexOf(newChip);
                        if (chipIndex == -1)
                            AppCanvas.Children.Add(newChip);
                        else
                            AppCanvas.Children[chipIndex] = newChip;


                        //DashLine
                        newLine = new Line();
                        newLine.X2 = appGraphVertex.Data.X;
                        newLine.Y2 = appGraphVertex.Data.Y;

                        newLine.X1 = neighbor.Vertex.Data.X;
                        newLine.Y1 = neighbor.Vertex.Data.Y;

                        newLine.Stroke = Brushes.White;
                        newLine.StrokeThickness = 5;
                        newLine.StrokeDashArray = doubleCollection;

                        //                        AppCanvas.Children.Add(newLine);
                        int dashlineIndex = AppCanvas.Children.IndexOf(newLine);
                        if (dashlineIndex == -1)
                            AppCanvas.Children.Add(newLine);
                        else
                            AppCanvas.Children[dashlineIndex] = newLine;
                    }
                }


            });
        }
        private void ComputeActiveCars()
        {
            this.Dispatcher.Invoke(() =>
            {
                foreach (Vehicle vehicle in VehicleList)
                {
                    if (vehicle.IsActive == false) continue;
                    if (vehicle.CurrLocation == null) vehicle.CurrLocation = vehicle.From;


                    //Initializer for next travel
                    if (vehicle.CurrDestination == null)
                    {
                        vehicle.CurrDestination = vehicle.TravelRoute.Pop();
                        //Sets Distance
                        foreach (Neighbor<LocationVertexPair> currLocationNeighbor in vehicle.CurrLocation.Neighbors)
                        {
                            if (currLocationNeighbor.GetVertex() == vehicle.CurrDestination)
                            {
                                vehicle.LocalDistance = currLocationNeighbor.Weight;
                                break;
                            }
                            
                        }

                    }

                    //Check if vehicle  has reached destination
                    if (vehicle.LocalProgress > vehicle.LocalDistance)
                    {
                        vehicle.CurrLocation = vehicle.CurrDestination;

                        //Vehicle has reached final destination
                        if (vehicle.CurrLocation == vehicle.To)
                        {
                            vehicle.IsActive = false;
                            DisplayToSnackBar($"{vehicle.Name} has Finished! Travelling");
                            continue;
                        }
                        vehicle.CurrDestination = vehicle.TravelRoute.Pop();
                        vehicle.TotalProgress += vehicle.LocalProgress;
                        vehicle.LocalProgress = 0;
                    }
                }
            });
        }

        //Draw Cars
        private void RenderCars()
        {
            this.Dispatcher.Invoke(() =>
            {

                foreach (Vehicle vehicle in VehicleList)
                {
                    if (!vehicle.IsActive) continue;
                    var tmpVehicleElement = vehicle.Element;
                    var tmpVehicleChip = vehicle.Chip;

                    //Sets car element angle
                    var deltaY = vehicle.CurrDestination.Data.Y - vehicle.CurrLocation.Data.Y;
                    var deltaX = vehicle.CurrDestination.Data.X - vehicle.CurrLocation.Data.X;

                    var tmpdeltaY = -deltaY;
                    var angle = Atan2(tmpdeltaY, deltaX) * (180 / PI);

                    var rotation = new RotateTransform();
                    rotation.Angle = -angle;
                    vehicle.Element.RenderTransformOrigin = new Point(0.5, 0.5);
                    vehicle.Element.RenderTransform = rotation;

                    //Move vehicle
                    vehicle.LocalProgress += vehicle.Speed * (ClockTimerPeriod / 1000);
                    var percentage = vehicle.LocalProgress / vehicle.LocalDistance;
                    vehicle.X = vehicle.CurrLocation.Data.X + (percentage) * deltaX;
                    vehicle.Y = vehicle.CurrLocation.Data.Y + (percentage) * deltaY;

                    Canvas.SetTop(tmpVehicleElement, vehicle.Y - 15);
                    Canvas.SetLeft(tmpVehicleElement, vehicle.X - 30);

                    Canvas.SetTop(tmpVehicleChip, vehicle.Y + 20);
                    Canvas.SetLeft(tmpVehicleChip, vehicle.X - 25);


                    
                    int chipIndex = AppCanvas.Children.IndexOf(tmpVehicleChip);
                    if (chipIndex == -1)
                        AppCanvas.Children.Add(tmpVehicleChip);
                    else
                        AppCanvas.Children[chipIndex] = tmpVehicleChip;

                    int vehicleIndex = AppCanvas.Children.IndexOf(tmpVehicleElement);
                    if (vehicleIndex == -1)
                        AppCanvas.Children.Add(tmpVehicleElement);
                    else
                        AppCanvas.Children[vehicleIndex] = tmpVehicleElement;


                }
            });
        }

        private void TimerDisplay()
        {
            this.Dispatcher.Invoke(() =>
            {
                if (TimeElapsed.Text == "Time Elapsed") TimeElapsed.Text = "0";
                double time = (ClockTimerPeriod / 1000) + Convert.ToDouble(TimeElapsed.Text);
                TimeElapsed.Text = time.ToString();

            });
        }
        private void ActivateAllVehicles()
        {
            foreach (Vehicle vehicle in VehicleList)
            {
                vehicle.IsActive = true;
            }
        }
        private async void DisplayToSnackBar(string input)
        {
            this.Dispatcher.Invoke(() =>
            {
                fms.Enqueue(input);
            });

        }
        private void PlayMusicClick(object sender, RoutedEventArgs e)
        {
            //Start
            if (!isPlaying)
            {
                mediaPlayer.Play();
                MusicTextBlock.Text = "Stop Music";
                isPlaying = true;
            }
            //Stop
            else
            {
                MusicTextBlock.Text = "Play Music";
                mediaPlayer.Pause();
                isPlaying = false;
            }
        }
        private void NextMusicClick(object sender, RoutedEventArgs e)
        {
            int index = random.Next(1, 10);
            string musicpath = "Music" + index + ".mp3";
            mediaPlayer.Open(new Uri(@musicpath, UriKind.Relative));
            mediaPlayer.Play();
        }

        #endregion



    }
}
