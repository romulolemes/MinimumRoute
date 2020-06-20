using MinimumRoute.Model;
using System.Collections.Generic;

namespace MinimumRoute.Data
{
    public class Context 
    {
        public Context()
        {
            Cities = new List<CityEntity>
            {
                new CityEntity("Los Santos", "LS"),
                new CityEntity("San Fierro", "SF"),
                new CityEntity("Las Venturas", "LV"),
                new CityEntity("Red County", "RC"),
                new CityEntity("Whetstone", "WS"),
                new CityEntity("Bone County", "BC")
            };
            Routes = new List<RouteEntity>();
        }

        public virtual List<CityEntity> Cities { get; }
        public virtual List<RouteEntity> Routes { get; }
    }
}
