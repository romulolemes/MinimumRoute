using MinimumRoute.Entity;
using MinimumRoute.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MinimumRoute.Algoritmo
{
    public interface IShortestPathFinder
    {
        PathEntity FindShortestPath(CityEntity from, CityEntity to);
    }
}
