using MinimumRoute.Model;

namespace MinimumRoute.Search.Dijkstra
{
    public struct NeighborhoodInfo
    {
        public Node Node { get; }
        public int WeightToNode { get; }

        public NeighborhoodInfo(Node node, int weightToNode)
        {
            Node = node;
            WeightToNode = weightToNode;
        }
    }
}
