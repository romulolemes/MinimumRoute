using MinimumRoute.Model;

namespace MinimumRoute.Search.Dijkstra
{
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
}
