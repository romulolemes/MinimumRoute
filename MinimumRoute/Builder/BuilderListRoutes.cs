using MinimumRoute.Model;
using MinimumRoute.Repository;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MinimumRoute.Builder
{
    public class BuilderListRoutes : IBuilderListRoutes
    {
        private const string SEPARATE = @"[\s]+";
        protected ICityRepository _cityRepository;

        public BuilderListRoutes(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public List<RouteEntity> CreateListRoutes(List<string> rowsRoutes)
        {
            List<RouteEntity> routes = new List<RouteEntity>();

            rowsRoutes.ForEach(row =>
            {
                routes.Add(CreateRoutes(row));
            });

            return routes;
        }

        private RouteEntity CreateRoutes(string row)
        {
            string[] result = Regex.Split(row, SEPARATE, RegexOptions.IgnoreCase);

            var cityOrigin = _cityRepository.FindByCode(result[0]);
            var cityDestination = _cityRepository.FindByCode(result[1]);
            var distance = int.Parse(result[2]);

            return new RouteEntity(cityOrigin, cityDestination, distance);
        }
    }
}
