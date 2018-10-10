using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using FinalProjectBase;

namespace FinalProjectApp.Models
{
    public class LocationVertexPair
    {

        public Vertex<String> Location { get; set; }
        public double X { get; set; }    
        public double Y { get; set; }
        public Shape Element { get; set; }

        public LocationVertexPair(Vertex<string> location, double x, double y, Shape element)
        {
            Location = location;
            X = x;
            Y = y;
            Element = element;
        }      

        public override string ToString()
        {
            return $"{Location} @ ({X},{Y})";
        }
    }
}
