using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;

namespace FinalProjectBase
{
    public class Graph<T>
    {
        public IList<Vertex<T>> Vertices { get; set; }

        public Graph()
        {
            Vertices = new List<Vertex<T>>();
        }

        public Graph(int capacity)
        {
            Vertices= new List<Vertex<T>>(capacity);
        }

        public void AddVertex(T data)
        {
            Vertices.Add(new Vertex<T>(data));
            Vertices[Vertices.Count - 1].ID = Vertices.Count - 1;
        }
        //Unweighted
        public void AddNeighbor(Vertex<T> fromVertex, Vertex<T> toVertex) => Vertices[fromVertex.ID].AddNeighbor(toVertex);
        public void AddNeighbor(int fromVertexID, int toVertexID) => Vertices[fromVertexID].AddNeighbor(Vertices[toVertexID]);
        //Weighted
        public void AddNeighbor(Vertex<T> fromVertex, Vertex<T> toVertex, double weight) => Vertices[fromVertex.ID].AddNeighbor(toVertex, weight);
        public void AddNeighbor(int fromVertexID, int toVertexID, double weight) => Vertices[fromVertexID].AddNeighbor(Vertices[toVertexID], weight);

        public Route<T> CalculateShortestRoutes(int fromVertexID)
        {
            bool[] visitedVerticesCheck = new bool[Vertices.Count];
            Route<T> resultantRoute = new Route<T>(Vertices.Count);

            resultantRoute.DisplacementList[fromVertexID] = 0;
            List<Vertex<T>> unvisitedVertices = new List<Vertex<T>>();
            unvisitedVertices.Add(Vertices[fromVertexID]);
            while (unvisitedVertices.Count != 0)
            {
                var targetVertex = unvisitedVertices[0];
                foreach (Vertex<T> vertex in unvisitedVertices)
                {
                    if (resultantRoute.DisplacementList[targetVertex.ID] > resultantRoute.DisplacementList[vertex.ID])
                        targetVertex = vertex;
                }
                unvisitedVertices.Remove(targetVertex);
                visitedVerticesCheck[targetVertex.ID] = true;

                foreach (Neighbor<T> targetVertexNeighbor in targetVertex.Neighbors)
                {
                    if (visitedVerticesCheck[targetVertexNeighbor.Vertex.ID] == true) continue;

                    if (resultantRoute.PredecessorList[targetVertexNeighbor.Vertex.ID] == null)
                        resultantRoute.PredecessorList[targetVertexNeighbor.Vertex.ID] = targetVertex;

                    if (targetVertexNeighbor.Weight +
                        resultantRoute.DisplacementList[targetVertex.ID] <
                        resultantRoute.DisplacementList[targetVertexNeighbor.Vertex.ID])
                    {
                        resultantRoute.DisplacementList[targetVertexNeighbor.Vertex.ID] =
                            targetVertexNeighbor.Weight + resultantRoute.DisplacementList[targetVertex.ID];

                        resultantRoute.PredecessorList[targetVertexNeighbor.Vertex.ID] = Vertices[targetVertex.ID];
                    }

                    if (visitedVerticesCheck[targetVertexNeighbor.Vertex.ID] == false) unvisitedVertices.Add(targetVertexNeighbor.Vertex);
                }
            }
            return resultantRoute;
        }

        public Route<T> DepthFirstSearch(int fromVertexID)
        {
            var initialVertex = Vertices[fromVertexID];
            bool[] visitedVerticesCheck = new bool[Vertices.Count];
            DepthFirstSearchRoute = new Route<T>(Vertices.Count);
            DepthFirstSearchRecursion(initialVertex, visitedVerticesCheck);
            return DepthFirstSearchRoute;
        }

        private Route<T> DepthFirstSearchRoute;
        private void DepthFirstSearchRecursion(Vertex<T> initialVertex, bool[] visitedVerticesCheck, double currDisplacement=0)
        {
            visitedVerticesCheck[initialVertex.ID] = true;
            DepthFirstSearchRoute.DisplacementList[initialVertex.ID] = currDisplacement;
            foreach (Neighbor<T> initialVertexNeighbor in initialVertex.Neighbors)
            {
                if (visitedVerticesCheck[initialVertexNeighbor.Vertex.ID] == false)
                {
                    DepthFirstSearchRoute.PredecessorList[initialVertexNeighbor.Vertex.ID] = initialVertex;
                    DepthFirstSearchRecursion(initialVertexNeighbor.Vertex, visitedVerticesCheck, initialVertexNeighbor.Weight+ currDisplacement);
                }
            }
        }
    }
}