using MinimumRoute.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MinimumRoute.Search.Dijkstra
{
    public class Weight
    {
        public CityEntity Origin { get; }
        public int Value { get; }

        public Weight(CityEntity origin, int value)
        {
            Origin = origin;
            Value = value;
        }
    }
}
