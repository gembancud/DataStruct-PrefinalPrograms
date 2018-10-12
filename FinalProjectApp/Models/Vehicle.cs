using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.TextFormatting;
using System.Windows.Shapes;
using FinalProjectBase;

namespace FinalProjectApp.Models
{
    public class Vehicle
    {
        public String Name { get; set; }
        public Vertex<LocationVertexPair> From { get; set; }
        public Vertex<LocationVertexPair> To { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Speed { get; set; }
        public bool IsActive { get; set; }
        public Route<LocationVertexPair> Route { get; set; }
        public Stack<Vertex<LocationVertexPair>> TravelRoute { get; set; }
        public double TotalDistance { get; set; }
        public double LocalDistance { get; set; }
        public Shape Element { get; set; }
        public double LocalProgress { get; set; }
        public double TotalProgress { get; set; }
        public Vertex<LocationVertexPair> CurrLocation { get; set; }
        public Vertex<LocationVertexPair> CurrDestination { get; set; }


        public Vehicle(string name, Vertex<LocationVertexPair> @from, Vertex<LocationVertexPair> to, double speed, Shape element)
        {
            Name = name;
            From = @from;
            To = to;
            X = @from.Data.X;
            Y = @from.Data.Y;
            Speed = speed;
            IsActive = false;
            Element = element;
        }

        // Creates the Order of Vertices to Travel to
        public void SetTravelRoute()
        {
            TravelRoute = new Stack<Vertex<LocationVertexPair>>();
            

            var tmpVertex = To;
            TravelRoute.Push(tmpVertex);
            while (tmpVertex != From)
            {
                if (tmpVertex == null) break;
                int index = tmpVertex.ID;
                tmpVertex = Route.PredecessorList[index];
                

                if (tmpVertex == From ||tmpVertex == null) break;
                TravelRoute.Push(tmpVertex);
            }
        }
        
    }
}
