using System.CodeDom;
using System.Collections.Generic;
using System.Security.Principal;

namespace FinalProjectBase
{
    public class Route<T>
    {
        public IList<Vertex<T>> PredecessorList { get; set; }
        public IList<double> DisplacementList { get; set; }

        public double TotalDisplacement
        {
            get
            {
                double total = 0;
                foreach (double displacement in DisplacementList)
                {
                    total += displacement;
                }

                return total;
            }
        }

        public Route(int count)
        {
            PredecessorList = new Vertex<T>[count];
            DisplacementList= new double[count];
            for (int i = 0; i < DisplacementList.Count; i++)
            {
                DisplacementList[i] = double.MaxValue;
            }
        }

        public double GetDisplacementOfVertex(Vertex<T> vertex)
        {
            return DisplacementList[vertex.ID];
        }

    }
}