using System.Collections.Generic;
using System.Security.Principal;

namespace FinalProjectBase
{
    public class Route<T>
    {
        public IList<Vertex<T>> PredecessorList { get; set; }
        public IList<double> DisplacementList { get; set; }

        public Route(int count)
        {
            PredecessorList = new Vertex<T>[count];
            DisplacementList= new double[count];
            for (int i = 0; i < DisplacementList.Count; i++)
            {
                DisplacementList[i] = double.MaxValue;
            }
        }
    }
}