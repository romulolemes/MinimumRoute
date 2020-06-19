using MinimumRoute.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MinimumRoute.Algoritmo
{
    public interface IShortestPathFinder
    {
        CityEntity[] FindShortestPath(CityEntity from, CityEntity to);
    }
}
