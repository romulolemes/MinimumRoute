using MinimumRoute.Entity;
using MinimumRoute.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MinimumRoute.Search.Dijkstra
{
    public class DijkstraAlgorithm : IShortestPathFinder
    {
        public PathEntity FindShortestPath(Node origin, Node destination, Func<Node, IEnumerable<NeighborhoodInfo>> funcGetNeighbors)
        {
            var vistingControl = new VisitingDataControl();
            vistingControl.UpdateWeight(origin, new Weight(null, 0));
            vistingControl.ScheduleVisitTo(origin);

            while (vistingControl.HasScheduledVisits)
            {
                var visitingNode = vistingControl.GetNodeToVisit();
                var visitingNodeWeight = vistingControl.QueryWeight(visitingNode);
                vistingControl.RegisterVisitTo(visitingNode);

                foreach (var neighborhoodInfo in funcGetNeighbors(visitingNode))
                {
                    if (!vistingControl.WasVisited(neighborhoodInfo.Node))
                    {
                        vistingControl.ScheduleVisitTo(neighborhoodInfo.Node);
                    }

                    var neighborWeight = vistingControl.QueryWeight(neighborhoodInfo.Node);

                    var probableWeight = visitingNodeWeight.Value + neighborhoodInfo.WeightToNode;
                    if (neighborWeight.Value > probableWeight)
                    {
                        vistingControl.UpdateWeight(neighborhoodInfo.Node, new Weight(visitingNode, probableWeight));
                    }
                }
            }

            PathEntity pathEntity;

            if (vistingControl.HasComputedPathToOrigin(destination))
            {
                pathEntity = new PathEntity
                {
                    NodeVisit = vistingControl.ComputedPathToOrigin(destination).Reverse().ToList(),
                    Distance = vistingControl.QueryWeight(destination).Value
                };
            }
            else
            {
                pathEntity = new PathEntity();
            }

            return pathEntity;
        }
    }
}
