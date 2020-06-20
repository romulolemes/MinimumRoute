using MinimumRoute.Entity;
using MinimumRoute.Model;
using MinimumRoute.Search.Dijkstra;
using System;
using System.Collections.Generic;

namespace MinimumRoute.Search
{
    public interface IShortestPathFinder
    {
        PathEntity FindShortestPath(Node from, Node to, Func<Node, IEnumerable<NeighborhoodInfo>> funcGetNeighbors);
    }
}