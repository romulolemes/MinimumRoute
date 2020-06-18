using System;
using System.Collections.Generic;
using System.Text;

namespace MinimumRoute.Model
{
    public class RouteEntity
    {
        public RouteEntity(CityEntity cityOrigin, CityEntity cityDestination, int distance)
        {
            CityOrigin = cityOrigin ?? throw new ArgumentNullException(nameof(cityOrigin));
            CityDestination = cityDestination ?? throw new ArgumentNullException(nameof(cityDestination));
            Distance = distance;
        }

        public CityEntity CityOrigin { get; set; }
        public CityEntity CityDestination { get; set; }
        public int Distance { get; set; }
    }
}
