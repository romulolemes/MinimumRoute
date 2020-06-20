using MinimumRoute.Model;
using MinimumRoute.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace MinimumRoute.Entity
{
    public class PathEntity : ISerializer
    {
        public PathEntity()
        {
            CitiesVisit = new HashSet<CityEntity>();
        }

        public ICollection<CityEntity> CitiesVisit { get; set; }
        public int? Distance { get; set; }
        public string Test { set { } }

        public string Serializer(Func<object, string> deserializeField)
        {
            StringBuilder textDeserialize = new StringBuilder();
            foreach (var city in CitiesVisit)
            {
                textDeserialize.Append(deserializeField(city.Code));
            }
            textDeserialize.Append(deserializeField(Distance));

            return textDeserialize.ToString();
        }

        public override string ToString()
        {
            return $"{string.Join(" ", CitiesVisit)} {Distance}";
        }
    }
}
