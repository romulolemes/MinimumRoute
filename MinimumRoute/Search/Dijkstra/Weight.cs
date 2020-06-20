using MinimumRoute.Model;

namespace MinimumRoute.Search.Dijkstra
{
    public class Weight
    {
        public Node Origin { get; }
        public int Value { get; }

        public Weight(Node origin, int value)
        {
            Origin = origin;
            Value = value;
        }
    }
}
