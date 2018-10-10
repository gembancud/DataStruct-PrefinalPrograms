using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProjectBase;

namespace FinalProjectApp.Models
{
    public class Vehicle
    {
        public Vertex<String> From { get; set; }
        public Vertex<String> To { get; set; }
        public Vertex<String> CurrentLocation { get; set; }
        public double Speed { get; set; }
    }
}
