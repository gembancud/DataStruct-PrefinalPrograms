using System.Collections.Generic;
using System.Text;

namespace FinalProjectBase
{
    public class Vertex<T>
    {
        public int ID { get; set; }
        public T Data { get; set; }
        public List<Neighbor<T>> Neighbors { get; set; }

        public Vertex(T data)
        {
            Data = data;
            Neighbors = new List<Neighbor<T>>();
        }

        public void AddNeighbor(Vertex<T> vertex) => Neighbors.Add(new Neighbor<T>(vertex,ID));
        public void AddNeighbor(Vertex<T> vertex, double weight) => Neighbors.Add(new Neighbor<T>(vertex,ID, weight));

        public override string ToString()
        {
            StringBuilder sb= new StringBuilder();
            sb.Append($"{Data}: ");
            foreach (Neighbor<T> neighbor in Neighbors)
            {
                sb.Append($"{neighbor.Vertex.Data}({neighbor.Weight}), ");
            }
            return sb.ToString();
        }
    }
}