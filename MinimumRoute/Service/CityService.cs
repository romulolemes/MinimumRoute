using MinimumRoute.Model;
using MinimumRoute.Repository;

namespace MinimumRoute.Service
{
    public class CityService 
    {
        public CityRepository _cityRepository;

        public CityService(CityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public CityEntity FindByCode(string code)
        {
            return _cityRepository.FindByCode(code);
        }
    }
}
