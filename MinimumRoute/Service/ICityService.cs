using MinimumRoute.Model;

namespace MinimumRoute.Service
{
    public interface ICityService
    {
        CityEntity FindByCode(string code);
    }
}