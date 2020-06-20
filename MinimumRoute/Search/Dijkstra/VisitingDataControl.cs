using MinimumRoute.Model;
using System.Collections.Generic;
using System.Linq;

namespace MinimumRoute.Search.Dijkstra
{
    public class VisitingDataControl
    {
        readonly List<Node> _visiteds;
        readonly Dictionary<Node, Weight> _weights;
        readonly List<Node> _scheduled;

        public VisitingDataControl()
        {
            _visiteds = new List<Node>();
            _weights = new Dictionary<Node, Weight>();
            _scheduled = new List<Node>();
        }

        public void RegisterVisitTo(Node node)
        {
            if (!_visiteds.Contains(node))
                _visiteds.Add(node);
        }

        public bool WasVisited(Node node)
        {
            return _visiteds.Contains(node);
        }

        public void UpdateWeight(Node node, Weight newWeight)
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

        public Weight QueryWeight(Node node)
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

        public void ScheduleVisitTo(Node node)
        {
            _scheduled.Add(node);
        }

        public bool HasScheduledVisits => _scheduled.Count > 0;

        public Node GetNodeToVisit()
        {
            var ordered = from n in _scheduled
                          orderby QueryWeight(n).Value
                          select n;

            var result = ordered.First();
            _scheduled.Remove(result);
            return result;
        }

        public bool HasComputedPathToOrigin(Node node)
        {
            return QueryWeight(node).Origin != null;
        }

        public IEnumerable<Node> ComputedPathToOrigin(Node node)
        {
            var n = node;
            while (n != null)
            {
                yield return n;
                n = QueryWeight(n).Origin;
            }
        }
    }
}
