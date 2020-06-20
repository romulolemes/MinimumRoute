using MinimumRoute.Model;
using System.Collections.Generic;
using System.Linq;

namespace MinimumRoute.Search.Dijkstra
{
    public class VisitingDataControl
    {
        readonly List<CityEntity> _visiteds;
        readonly Dictionary<CityEntity, Weight> _weights;
        readonly List<CityEntity> _scheduled;

        public VisitingDataControl()
        {
            _visiteds = new List<CityEntity>();
            _weights = new Dictionary<CityEntity, Weight>();
            _scheduled = new List<CityEntity>();
        }

        public void RegisterVisitTo(CityEntity node)
        {
            if (!_visiteds.Contains(node))
                _visiteds.Add(node);
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
            return QueryWeight(node).Origin != null;
        }

        public IEnumerable<CityEntity> ComputedPathToOrigin(CityEntity node)
        {
            var n = node;
            while (n != null)
            {
                yield return n;
                n = QueryWeight(n).Origin;
            }
        }

        public IEnumerable<NeighborhoodInfo> GetNeighbors(CityEntity visitingNode)
        {
            return visitingNode.RouteOrigin.Select(r => new NeighborhoodInfo(r.CityDestination, r.Distance));
        }
    }
}
