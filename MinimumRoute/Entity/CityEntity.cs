using System;
using System.Collections.Generic;

namespace MinimumRoute.Model
{
    public class CityEntity
    {
        public CityEntity(string name, string code)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Code = code ?? throw new ArgumentNullException(nameof(code));
            RouteOrigin = new HashSet<RouteEntity>();
            RouteDestination = new HashSet<RouteEntity>();
        }

        public string Name { get; set; }
        public string Code { get; set; }

        public HashSet<RouteEntity> RouteOrigin { get; set; }
        public HashSet<RouteEntity> RouteDestination { get; set; }

        public override string ToString()
        {
            return $"{Name}-{Code}";
        }
    }
}
