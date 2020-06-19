using MinimumRoute.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MinimumRoute.Entity
{
    public class PathEntity
    {
        public PathEntity()
        {
            CitiesVisit = new HashSet<CityEntity>();
        }

        public ICollection<CityEntity> CitiesVisit { get; set; }
        public int? Distance { get; set; }

        public override string ToString()
        {
            return $"{string.Join(" ", CitiesVisit)} {Distance}";
        }
    }
}
