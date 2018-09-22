using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectBase
{
    class Program
    {
        static void Main(string[] args)
        {

            Graph<string> stringGraph = new Graph<string>();
            stringGraph.AddVertex("A");
            stringGraph.AddVertex("B");
            stringGraph.AddVertex("C");
            stringGraph.AddVertex("D");
            stringGraph.AddVertex("E");

            stringGraph.AddNeighbor(0, 1, 6);
            stringGraph.AddNeighbor(1, 0, 6);

            stringGraph.AddNeighbor(0, 3, 1);
            stringGraph.AddNeighbor(3, 0, 1);

            stringGraph.AddNeighbor(1, 3, 2);
            stringGraph.AddNeighbor(3, 1, 2);

            stringGraph.AddNeighbor(3, 4, 1);
            stringGraph.AddNeighbor(4, 3, 1);

            stringGraph.AddNeighbor(1, 4, 2);
            stringGraph.AddNeighbor(4, 1, 2);

            stringGraph.AddNeighbor(1, 2, 5);
            stringGraph.AddNeighbor(2, 1, 5);

            stringGraph.AddNeighbor(4, 2, 5);
            stringGraph.AddNeighbor(2, 4, 5);

            var x =stringGraph.CalculateShortestRoutes(0);

            var y = stringGraph.DepthFirstSearch(0);
            Console.Read();
        }
    }
}
