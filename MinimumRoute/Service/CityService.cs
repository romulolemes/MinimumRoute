using MinimumRoute.Model;
using MinimumRoute.Repository;

namespace MinimumRoute.Service
{
    public class CityService : ICityService
    {
        public ICityRepository _cityRepository;

        public CityService(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public CityEntity FindByCode(string code)
        {
            return _cityRepository.FindByCode(code);
        }
    }
}
