using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace FinalProjectBase
{
    public class Neighbor<T>:IComparable<Neighbor<T>>
    {
        public Vertex<T> Vertex { get; set; }
        public double Weight { get; set; }
        public int PredID { get; set; }

        public Neighbor(Vertex<T> vertex, int predID, double weight)
        {
            Vertex = vertex;
            Weight = weight;
            PredID = predID;
        }

        public Neighbor(Vertex<T> vertex ,int predID)
        {
            Vertex = vertex;
            Weight = 0;
            PredID = predID;
        }

        public int CompareTo(Neighbor<T> other)
        {
            return this.Weight.CompareTo(other.Weight);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{Vertex.Data}: ");
            foreach (Neighbor<T> vertexNeighbor in Vertex.Neighbors)
            {
                sb.Append($"{vertexNeighbor.Vertex.Data}({vertexNeighbor.Weight}), ");
            }

            return sb.ToString();
        }


    }
}