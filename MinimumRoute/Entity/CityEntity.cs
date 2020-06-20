using MinimumRoute.Search.Dijkstra;
using System;

namespace MinimumRoute.Model
{
    public class CityEntity : Node
    {
        public CityEntity(string name, string code)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Code = code ?? throw new ArgumentNullException(nameof(code));
        }

        public string Name { get; set; }

        public override string ToString()
        {
            return $"{Name}-{Code}";
        }
    }
}
