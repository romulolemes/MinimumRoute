using MinimumRoute.Model;
using MinimumRoute.Repository;
using System.Text.RegularExpressions;

namespace MinimumRoute.Builder
{
    public class BuilderRoute
    {
        private const string SEPARATE = @"[\s]+";
        protected ICityRepository _cityRepository;

        public BuilderRoute(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public RouteEntity Create(string row)
        {
            string[] result = Regex.Split(row, SEPARATE, RegexOptions.IgnoreCase);

            var cityOrigin = _cityRepository.FindByCode(result[0]);
            var cityDestination = _cityRepository.FindByCode(result[1]);
            var distance = int.Parse(result[2]);

            return new RouteEntity(cityOrigin, cityDestination, distance);
        }
    }
}
