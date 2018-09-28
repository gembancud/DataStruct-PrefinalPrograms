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

            //Testcase for number 5
            //stringGraph.AddVertex("LA15");//0
            //stringGraph.AddVertex("LA16");//1
            //stringGraph.AddVertex("LA22");//2
            //stringGraph.AddVertex("LA31");//3
            //stringGraph.AddVertex("LA32");//4
            //stringGraph.AddVertex("LA126");//5
            //stringGraph.AddVertex("LA127");//6
            //stringGraph.AddVertex("LA141");//7
            //stringGraph.AddVertex("LA169");//8

            //stringGraph.AddNeighbor(0,2);
            //stringGraph.AddNeighbor(2, 0);
            
            //stringGraph.AddNeighbor(0,1);
            //stringGraph.AddNeighbor(0, 3);

            //stringGraph.AddNeighbor(1,4);
            //stringGraph.AddNeighbor(3, 4);

            //stringGraph.AddNeighbor(2,5);
            //stringGraph.AddNeighbor(4, 5);

            //stringGraph.AddNeighbor(1, 6);

            //stringGraph.AddNeighbor(2,7);
            //stringGraph.AddNeighbor(1, 7);

            //stringGraph.AddNeighbor(4,8);

            //var result = stringGraph.AllConnectedUnweightedRoutes();

            //Testcase for number 6
            stringGraph.AddVertex("1");
            stringGraph.AddVertex("2");
            stringGraph.AddVertex("3");
            stringGraph.AddVertex("4");
            stringGraph.AddVertex("5");
            stringGraph.AddVertex("6");
            stringGraph.AddVertex("7");
            stringGraph.AddVertex("8");

            stringGraph.AddUndirectedNeighbor(0,1,240);
            stringGraph.AddUndirectedNeighbor(0,2,210);
            stringGraph.AddUndirectedNeighbor(0,3,340);
            stringGraph.AddUndirectedNeighbor(0,4,280);
            stringGraph.AddUndirectedNeighbor(0,5,200);
            stringGraph.AddUndirectedNeighbor(0,6,345);
            stringGraph.AddUndirectedNeighbor(0,7,120);

            stringGraph.AddUndirectedNeighbor(1, 2, 265);
            stringGraph.AddUndirectedNeighbor(1, 3, 175);
            stringGraph.AddUndirectedNeighbor(1, 4, 215);
            stringGraph.AddUndirectedNeighbor(1, 5, 180);
            stringGraph.AddUndirectedNeighbor(1, 6, 185);
            stringGraph.AddUndirectedNeighbor(1, 7, 155);

            stringGraph.AddUndirectedNeighbor(2, 3, 260);
            stringGraph.AddUndirectedNeighbor(2, 4, 115);
            stringGraph.AddUndirectedNeighbor(2, 5, 350);
            stringGraph.AddUndirectedNeighbor(2, 6, 435);
            stringGraph.AddUndirectedNeighbor(2, 7, 195);

            stringGraph.AddUndirectedNeighbor(3, 4, 160);
            stringGraph.AddUndirectedNeighbor(3, 5, 330);
            stringGraph.AddUndirectedNeighbor(3, 6, 295);
            stringGraph.AddUndirectedNeighbor(3, 7, 230);

            stringGraph.AddUndirectedNeighbor(4, 5, 360);
            stringGraph.AddUndirectedNeighbor(4, 6, 400);
            stringGraph.AddUndirectedNeighbor(4, 7, 170);

            stringGraph.AddUndirectedNeighbor(5, 6, 175);
            stringGraph.AddUndirectedNeighbor(5, 7, 205);

            stringGraph.AddUndirectedNeighbor(6, 7, 305);
            var y = stringGraph.CalculateShortestRoutes(7);
            var x = stringGraph.LowestCostRoute();

            Console.Read();
        }
    }
}
