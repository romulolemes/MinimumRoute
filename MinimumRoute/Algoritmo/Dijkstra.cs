using MinimumRoute.Entity;
using MinimumRoute.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MinimumRoute.Algoritmo
{
    public class Dijkstra : IShortestPathFinder
    {
        private class Weight
        {
            public CityEntity From { get; }
            public int Value { get; }

            public Weight(CityEntity @from, int value)
            {
                From = @from;
                Value = value;
            }
        }

        public struct NeighborhoodInfo
        {
            public CityEntity Node { get; }
            public int WeightToNode { get; }

            public NeighborhoodInfo(CityEntity node, int weightToNode)
            {
                Node = node;
                WeightToNode = weightToNode;
            }
        }

        private class VisitingData
        {
            readonly List<CityEntity> _visiteds =
                new List<CityEntity>();

            readonly Dictionary<CityEntity, Weight> _weights =
                new Dictionary<CityEntity, Weight>();

            readonly List<CityEntity> _scheduled =
                new List<CityEntity>();

            public void RegisterVisitTo(CityEntity node)
            {
                if (!_visiteds.Contains(node))
                    _visiteds.Add((node));
            }

            public bool WasVisited(CityEntity node)
            {
                return _visiteds.Contains(node);
            }

            public void UpdateWeight(CityEntity node, Weight newWeight)
            {
                if (!_weights.ContainsKey(node))
                {
                    _weights.Add(node, newWeight);
                }
                else
                {
                    _weights[node] = newWeight;
                }
            }

            public Weight QueryWeight(CityEntity node)
            {
                Weight result;
                if (!_weights.ContainsKey(node))
                {
                    result = new Weight(null, int.MaxValue);
                    _weights.Add(node, result);
                }
                else
                {
                    result = _weights[node];
                }
                return result;
            }

            public void ScheduleVisitTo(CityEntity node)
            {
                _scheduled.Add(node);
            }

            public bool HasScheduledVisits => _scheduled.Count > 0;

            public CityEntity GetNodeToVisit()
            {
                var ordered = from n in _scheduled
                              orderby QueryWeight(n).Value
                              select n;

                var result = ordered.First();
                _scheduled.Remove(result);
                return result;
            }

            public bool HasComputedPathToOrigin(CityEntity node)
            {
                return QueryWeight(node).From != null;
            }

            public IEnumerable<CityEntity> ComputedPathToOrigin(CityEntity node)
            {
                var n = node;
                while (n != null)
                {
                    yield return n;
                    n = QueryWeight(n).From;
                }
            }

            public IEnumerable<NeighborhoodInfo> GetNeighbors(CityEntity visitingNode)
            {
                return visitingNode.RouteOrigin.Select(r => new NeighborhoodInfo(r.CityDestination, r.Distance));
            }
        }

        public PathEntity FindShortestPath(CityEntity @from, CityEntity to)
        {
            var control = new VisitingData();

            control.UpdateWeight(@from, new Weight(null, 0));
            control.ScheduleVisitTo(@from);

            while (control.HasScheduledVisits)
            {
                var visitingNode = control.GetNodeToVisit();
                var visitingNodeWeight = control.QueryWeight(visitingNode);
                control.RegisterVisitTo(visitingNode);

                foreach (var neighborhoodInfo in control.GetNeighbors(visitingNode))
                {
                    if (!control.WasVisited(neighborhoodInfo.Node))
                    {
                        control.ScheduleVisitTo(neighborhoodInfo.Node);
                    }

                    var neighborWeight = control.QueryWeight(neighborhoodInfo.Node);

                    var probableWeight = (visitingNodeWeight.Value + neighborhoodInfo.WeightToNode);
                    if (neighborWeight.Value > probableWeight)
                    {
                        control.UpdateWeight(neighborhoodInfo.Node, new Weight(visitingNode, probableWeight));
                    }
                }
            }

            PathEntity pathEntity;

            if (control.HasComputedPathToOrigin(to))
            {
                pathEntity = new PathEntity
                {
                    CitiesVisit = control.ComputedPathToOrigin(to).Reverse().ToList(),
                    Distance = control.QueryWeight(to).Value
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
