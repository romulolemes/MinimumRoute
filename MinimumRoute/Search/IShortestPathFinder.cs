using MinimumRoute.Entity;
using MinimumRoute.Model;

namespace MinimumRoute.Search
{
    public interface IShortestPathFinder
    {
        PathEntity FindShortestPath(CityEntity from, CityEntity to);
    }
}